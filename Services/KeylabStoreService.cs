using MidtermProject.Models;

namespace MidtermProject.Services;

public class KeylabStoreService
{
    private List<Product> _products = new()
    {
        new Product {
            Id = 1, Name = "Void 65 Keyboard", Category = "Keyboards", Price = 149.00m, Rating = 4.9, ReviewCount = 128,
            PrimaryImageUrl = "https://images.unsplash.com/photo-1587829741301-dc798b83add3?w=600&auto=format&fit=crop",
            AdditionalImages = new() {
                "https://images.unsplash.com/photo-1587829741301-dc798b83add3?w=600&auto=format&fit=crop",
                "https://images.unsplash.com/photo-1618384887929-16ec33fab9ef?w=600&auto=format&fit=crop",
                "https://images.unsplash.com/photo-1595225476474-87563907a212?w=600&auto=format&fit=crop",
                "https://images.unsplash.com/photo-1541140532154-b024d705b909?w=600&auto=format&fit=crop"
            },
            Description = "A gasket-mounted 65% board with CNC aluminum case, hot-swap sockets and pre-lubed linear switches.",
            Layout = "65%", Color = "White"
        },
        new Product {
            Id = 2, Name = "Slate 75 Wireless", Category = "Keyboards", Price = 189.00m, Rating = 4.8, ReviewCount = 128,
            PrimaryImageUrl = "https://images.unsplash.com/photo-1618384887929-16ec33fab9ef?w=600&auto=format&fit=crop",
            AdditionalImages = new() { "https://images.unsplash.com/photo-1618384887929-16ec33fab9ef?w=600&auto=format&fit=crop" },
            Description = "Clean, low-profile mechanical keyboard with multi-device Bluetooth and 2.4Ghz connectivity.",
            Layout = "75%", Color = "Grey", IsOnSale = true, BadgeText = "Sale"
        },
        new Product {
            Id = 3, Name = "Pebble TKL", Category = "Keyboards", Price = 129.00m, Rating = 5.0, ReviewCount = 128,
            PrimaryImageUrl = "https://images.unsplash.com/photo-1595225476474-87563907a212?w=600&auto=format&fit=crop",
            AdditionalImages = new() { "https://images.unsplash.com/photo-1595225476474-87563907a212?w=600&auto=format&fit=crop" },
            Description = "Tenkeyless mechanical board built for focus work and quiet tactile feedback.",
            Layout = "TKL", Color = "Black"
        },
        new Product {
            Id = 4, Name = "Aurora 60 Kit", Category = "Keyboards", Price = 99.00m, Rating = 4.7, ReviewCount = 128,
            PrimaryImageUrl = "https://images.unsplash.com/photo-1541140532154-b024d705b909?w=600&auto=format&fit=crop",
            AdditionalImages = new() { "https://images.unsplash.com/photo-1541140532154-b024d705b909?w=600&auto=format&fit=crop" },
            Description = "Custom DIY kit for enthusiasts looking to build their own compact 60% setup.",
            Layout = "60%", Color = "White"
        },
        new Product {
            Id = 5, Name = "Monolith 96", Category = "Keyboards", Price = 219.00m, Rating = 4.9, ReviewCount = 120,
            PrimaryImageUrl = "https://images.unsplash.com/photo-1587829741301-dc798b83add3?w=600&auto=format&fit=crop",
            AdditionalImages = new() { "https://images.unsplash.com/photo-1587829741301-dc798b83add3?w=600&auto=format&fit=crop" },
            Description = "Full-size functional layout with integrated volume knob and solid aluminum plate.",
            Layout = "Full", Color = "White", IsOnSale = true, BadgeText = "Sale"
        },
        new Product {
            Id = 6, Name = "Drift 65 Low-Profile", Category = "Keyboards", Price = 159.00m, Rating = 4.8, ReviewCount = 128,
            PrimaryImageUrl = "https://images.unsplash.com/photo-1618384887929-16ec33fab9ef?w=600&auto=format&fit=crop",
            AdditionalImages = new() { "https://images.unsplash.com/photo-1618384887929-16ec33fab9ef?w=600&auto=format&fit=crop" },
            Description = "Ultra-slim ergonomic keycaps designed for mobile typists and creators.",
            Layout = "65%", Color = "Black"
        }
    };

    private List<CartItem> _cart = new();

    public event Action? OnStateChange;

    public string CurrentUser { get; private set; } = string.Empty;
    public bool IsLoggedIn => !string.IsNullOrEmpty(CurrentUser);

    public List<Product> GetProducts() => _products;

    public Product? GetProductById(int id) => _products.FirstOrDefault(p => p.Id == id);

    public List<CartItem> GetCart() => _cart;

    public void AddToCart(Product product, string color = "Black", string switchType = "Linear", int qty = 1)
    {
        var existing = _cart.FirstOrDefault(c => c.Product.Id == product.Id && c.SelectedColor == color && c.SelectedSwitch == switchType);
        if (existing != null)
        {
            existing.Quantity += qty;
        }
        else
        {
            _cart.Add(new CartItem { Product = product, SelectedColor = color, SelectedSwitch = switchType, Quantity = qty });
        }
        NotifyStateChanged();
    }

    public void RemoveFromCart(CartItem item)
    {
        _cart.Remove(item);
        NotifyStateChanged();
    }

    public int GetCartCount() => _cart.Sum(c => c.Quantity);

    public decimal GetCartTotal() => _cart.Sum(c => c.Subtotal);

    public void Login(string username)
    {
        CurrentUser = username;
        NotifyStateChanged();
    }

    public void Logout()
    {
        CurrentUser = string.Empty;
        NotifyStateChanged();
    }

    private void NotifyStateChanged() => OnStateChange?.Invoke();
}