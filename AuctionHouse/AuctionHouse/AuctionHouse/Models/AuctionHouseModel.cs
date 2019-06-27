namespace AuctionHouse.Models
{
    using AuctionHouse.Models.Tables;
    using AuctionHouse.Models.DisplayModels;
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Collections.Generic;

    public partial class AuctionHouseModel : DbContext
    {
        public AuctionHouseModel()
            : base("name=AuctionHouseModel")
        {
        }

        public virtual DbSet<Auction> Auctions { get; set; }
        public virtual DbSet<Bid> Bids { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<TokenOrder> TokenOrders { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<SystemParameter> SystemParameters { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Auction>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Auction>()
                .Property(e => e.owner)
                .IsUnicode(false);

            modelBuilder.Entity<Auction>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<Auction>()
                .Property(e => e.won)
                .IsUnicode(false);

            modelBuilder.Entity<Auction>()
                .HasMany(e => e.Bids)
                .WithRequired(e => e.Auction)
                .HasForeignKey(e => e.auction_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Bid>()
                .Property(e => e.bidder)
                .IsUnicode(false);

            modelBuilder.Entity<TokenOrder>()
                .Property(e => e.orderer)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.first_name)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.last_name)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Owned)
                .WithRequired(e => e.Owner)
                .HasForeignKey(e => e.owner)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Won)
                .WithOptional(e => e.Won)
                .HasForeignKey(e => e.won)
                .WillCascadeOnDelete();

            modelBuilder.Entity<User>()
                .HasMany(e => e.Bids)
                .WithRequired(e => e.Bidder)
                .HasForeignKey(e => e.bidder);

            modelBuilder.Entity<User>()
                .HasMany(e => e.TokenOrders)
                .WithRequired(e => e.Orderer)
                .HasForeignKey(e => e.orderer);

            modelBuilder.Entity<SystemParameter>()
                .Property(e => e.C)
                .IsFixedLength();
        }

        private void UpdateState()
        {
            foreach (Auction a in Auctions)
            {
                if (a.closed == null && a.opened != null && ((DateTime)a.opened).AddSeconds(a.duration) <= DateTime.Now) { 
                    Bid lastBid = (from b in Bids
                                   where b.auction_id == a.id
                                   select b).OrderByDescending(b => b.amount).FirstOrDefault();
                    if (lastBid != null)
                    {
                        a.won = lastBid.bidder;
                        Entry(a).Reference(e => e.Won).Load();
                        a.Won.tokens_amount -= lastBid.amount;
                        a.Owner.tokens_amount += lastBid.amount;
                    }
                    a.closed = ((DateTime)a.opened).AddSeconds(a.duration);
                    a.state = "COMPLETED";
                }
            }

            SaveChanges();
        }

        public SystemParameter GetSystemParameters()
        {
            var query =
               from sp in SystemParameters
               select sp;

            return query.SingleOrDefault();
        }

        public PartialSystemParameters GetPartialSystemParameters()
        {
            return SystemParameters.Select(sp => new PartialSystemParameters
                        {
                            N = sp.N,
                            D = sp.D,
                            S = sp.S,
                            G = sp.G,
                            P = sp.P,
                            C = sp.C,
                            T = sp.T
                        }).SingleOrDefault();

        }

        public User FindUser(string email)
        {
            var query =
                from user in Users
                where user.email == email
                select user;

            return query.SingleOrDefault();
        }

        public Bid GetLastBid(Guid auction_id)
        {
            var query =
                from b in Bids
                where b.auction_id == auction_id
                select b;

            return query
                   .OrderByDescending(b => b.amount)
                   .FirstOrDefault();
        }

        public Auction GetAuction(Guid id)
        {
            var query =
                from auction in Auctions
                where auction.id == id
                select auction;

            return query.SingleOrDefault();
        }

        public TokenOrder GetTokenOrder(Guid id)
        {
            var query =
                from order in TokenOrders
                where order.id == id
                select order;

            return query.SingleOrDefault();
        }

        public float GetReservedTokens(string email)
        {
            var bids =
                (from bid in Bids
                 where bid.bidder == email &&
                       bid.Auction.state == "OPENED" &&
                       bid.Auction.Bids.OrderByDescending(b => b.amount).FirstOrDefault().id == bid.id
                 select bid.amount).ToList();

            return bids.Sum();
        }

        public float GetAvailableTokens(string email)
        {
            return FindUser(email).tokens_amount - GetReservedTokens(email);
        }
        
        public UserDetails GetUserDetails(string email)
        {
            float reserved_tokens = GetReservedTokens(email);
            return Users.Where(u => u.email == email)
                .Select(u => new UserDetails
                {
                    User = new PartialUser
                    {
                        email = u.email,
                        first_name = u.first_name,
                        last_name = u.last_name,
                        tokens_amount = u.tokens_amount,
                        available_tokens = u.tokens_amount - reserved_tokens,
                        is_administrator = u.is_administrator
                    },
                    Bids = u.Bids.OrderByDescending(b => b.created).Select(bb => new PartialBid
                    {
                        auction_name = bb.Auction.name,
                        amount = bb.amount,
                        created = bb.created
                    }).ToList(),

                    TokenOrders = u.TokenOrders.Select(t => new PartialTokenOrder
                    {
                        amount = t.amount,
                        price = t.price,
                        state = t.state
                    }).ToList()

                }).SingleOrDefault();

        }

        public IEnumerable<AuctionLastBid> GetAuctionsWithLastBid(int amount, int page,
                                                                  string regex = null,
                                                                  string state = null,
                                                                  float? max_price = null,
                                                                  float? min_price = null,
                                                                  string won = null, string owned = null)
        {
            var query = from a in Auctions
                        select a;

            if (regex != null)
            {
                query = query.Where(a => a.name.Contains(regex));
            }
            if (state != null && state != "")
            {
                query = query.Where(a => a.state == state);
            }
            if (max_price != null)
            {
                query = query.Where(a => (a.Bids.Count > 0 ? a.Bids.Max(b => b.amount) : a.starting_price) <= max_price);
            }
            if (min_price != null)
            {
                query = query.Where(a => (a.Bids.Count > 0 ? a.Bids.Max(b => b.amount) : a.starting_price) >= min_price);
            }
            if (won != null)
            {
                query = query.Where(a => (a.Won != null ? a.Won.email : null) == won);
            }
            if (owned != null)
            {
                query = query.Where(a => a.Owner.email == owned);
            }

            var auctions = query
                .OrderByDescending(a => a.opened)
                .Skip(page * amount)
                .Take(amount)
                .Select(a => new AuctionLastBid
                {
                    Auction = new PartialAuction
                    {
                        id = a.id,
                        name = a.name,
                        starting_price = a.starting_price,
                        duration = a.duration,
                        owner = a.owner,
                        description = a.description,
                        state = a.state,
                        created = a.created,
                        opened = a.opened,
                        closed = a.closed,
                        won = a.won
                    },
                    LastBid = a.Bids.OrderByDescending(b => b.amount).Select(pb => new PartialBid
                    {
                        bidder = pb.bidder,
                        amount = pb.amount,
                        firstName = pb.Bidder.first_name,
                        lastName = pb.Bidder.last_name
                    }).FirstOrDefault()
                }).ToList();

            UpdateState();

            return auctions;
        }

        public AuctionAllBids GetAuctionWithAllBids(Guid id)
        {
            return Auctions.Where(a => a.id == id)
                .Select(a => new AuctionAllBids
                {
                    Auction = new PartialAuction
                    {
                        id = a.id,
                        name = a.name,
                        starting_price = a.starting_price,
                        duration = a.duration,
                        owner = a.owner,
                        description = a.description,
                        state = a.state,
                        created = a.created,
                        opened = a.opened,
                        closed = a.closed,
                        won = a.won
                    },
                    Bids = a.Bids.OrderByDescending(b => b.amount).Select(bb => new PartialBid
                    {
                        bidder = bb.bidder,
                        amount = bb.amount,
                        firstName = bb.Bidder.first_name,
                        lastName = bb.Bidder.last_name,
                        created = bb.created
                    }).ToList()
                }).SingleOrDefault();
        }
    }
}
