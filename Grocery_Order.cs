// Grant Cates - INMT 443
// Grocery Store Project



using System;
//Creating a class for item
class Item
{
    public string ItemName { get; set; }
    public double Price { get; set; }
    public int QuantityOnHand { get; set; }
}
//Creating a class for Customer
class Customer
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
}
//Creating a class for ShoppingCartItem
class ShoppingCartItem
{
    public Item Product { get; set; }
    public int Quantity { get; set; }
}
//Creating a class for Order
class Order
{
    public Customer Customer { get; set; }
    public DateTime OrderDate { get; set; }
    public List<ShoppingCartItem> ShoppingCartItems { get; set; }
}

// Lets Begin :)

class Program

{   // Creating the Main Method
    static void Main(string[] args)
    {
        List<Item> products = InitializeProducts();//creating the list of items

        Customer customer = new Customer(); // creating a new customer
        CaptureCustomerInformation(customer);

        List<ShoppingCartItem> shoppingCart = new List<ShoppingCartItem>(); // creates new cart, and displays items for sale
        DisplayItemsForSale(products);
        AddItemsToCart(products, shoppingCart);

        Order order = new Order // this is the users order
        {
            Customer = customer, //Customer
            OrderDate = DateTime.Now, //CurrentDate Time
            ShoppingCartItems = shoppingCart //ShoppingCart
        };

        DisplayOrderSummary(order); // this calls the users order from below
        ThankYouMessage(customer); // this calls the users input, and outputs a ThankYou message back to the user
    }
    //Method 1
    static List<Item> InitializeProducts()
    {
        // Initialize and return a list of available items for sale (products).
        List<Item> products = new List<Item> // this code creates the new list with the Grocery data hardcoded below
        {
            new Item { ItemName = "Plums", Price = 1.75, QuantityOnHand = 100 },
            new Item { ItemName = "Apples", Price = 2.00, QuantityOnHand = 200 },
            new Item { ItemName = "Oranges", Price = 3.25, QuantityOnHand = 300 },
            new Item { ItemName = "Pears", Price = 4.00, QuantityOnHand = 100 },
            new Item { ItemName = "Bananas", Price = 5.25, QuantityOnHand = 300 },
            new Item { ItemName = "Grapes", Price = 6.50, QuantityOnHand = 200 },
            new Item { ItemName = "Kiwi", Price = 7.00, QuantityOnHand = 300 }
        };

        return products; //return products
    }
    //Method 2
    static void DisplayItemsForSale(List<Item> products)
    {
        Console.WriteLine("Available Items for Sale:"); // prompts user of the products for sale
        foreach (var product in products)
        {
            Console.WriteLine($"Name: {product.ItemName}, Price: ${product.Price}, Quantity Available: {product.QuantityOnHand}"); // write ItemName, UnitCost, and Quantity Available
        }
    }
    //Method 3
    static void CaptureCustomerInformation(Customer customer)
    {
        Console.Write("Enter your first name: "); //prompt user for first name
        customer.FirstName = Console.ReadLine();

        Console.Write("Enter your last name: "); //prompt user for last name
        customer.LastName = Console.ReadLine();

        Console.Write("Enter your email address: "); // prompt user for their email address
        customer.Email = Console.ReadLine();

        Console.Write("Enter your phone number: "); //prompt user for their phone number
        customer.PhoneNumber = Console.ReadLine();
    }
    //Method 4
    static void AddItemsToCart(List<Item> products, List<ShoppingCartItem> shoppingCart)
    {
        bool continueShopping = true; // if the user would like to contuine shopping

        while (continueShopping) //create a while loop for the items avaliabe for purchase
        {
            Console.Write("Enter the name of the item you want to purchase: "); //Display the prompt to the user
            string itemName = Console.ReadLine();

            Item product = products.Find(p => p.ItemName.Equals(itemName, StringComparison.OrdinalIgnoreCase)); //Validation of the Items

            if (product != null && product.QuantityOnHand > 0 && product.Price > 0)
            {
                Console.Write($"Enter the quantity of {itemName} you want to purchase: "); //Display the prompt to the user asking how many they would like
                int quantity = int.Parse(Console.ReadLine());

                if (quantity > 0 && quantity <= product.QuantityOnHand)
                {
                    ShoppingCartItem cartItem = new ShoppingCartItem
                    {
                        Product = product,
                        Quantity = quantity
                    };

                    shoppingCart.Add(cartItem); //Add item to the users shopping cart 

                    // Update the product quantity in the inventory.
                    product.QuantityOnHand -= quantity;

                    Console.WriteLine($"{quantity} {itemName}(s) added to your cart."); //Display prompt to user, added item to cart
                }
                else
                {
                    Console.WriteLine("Invalid quantity or not enough stock available."); //Display prompt to user, if user entered an invaild quantity
                }
            }
            else
            {
                Console.WriteLine("Invalid item name or item not available.");
            }

            Console.Write("Do you want to continue shopping? (Yes/No): ");
            string response = Console.ReadLine();
            continueShopping = response.Equals("Yes", StringComparison.OrdinalIgnoreCase);
        }
    }
    //Method 5
    static void DisplayOrderSummary(Order order)
    {
        Console.WriteLine("\nOrder Summary:"); //Display the Order Summary for user
        Console.WriteLine($"Customer: {order.Customer.FirstName} {order.Customer.LastName}"); //Display the user first and last name
        Console.WriteLine($"Order Date: {order.OrderDate}"); //Display the time which the users order was places

        double subTotal = 0.0;

        foreach (var cartItem in order.ShoppingCartItems)
        {
            Item product = cartItem.Product;
            double itemTotal = product.Price * cartItem.Quantity;
            subTotal += itemTotal;

            Console.WriteLine($"Item: {product.ItemName}, Quantity: {cartItem.Quantity}, " +
                $"Unit Price: ${product.Price}, Total: ${itemTotal}"); //Displays the Item, Quantity, Unit Price, and total amount of items per fruit the user is purchasing 
        }

        double shippingCost = 8.99; // Static shipping cost
        double taxRate = 0.0925; // 9.25% tax rate

        double tax = subTotal * taxRate; //Calulates the tax amount owed
        double orderTotal = subTotal + shippingCost + tax; //Calulates the Total amount owed at check out

        Console.WriteLine($"Subtotal: ${subTotal}"); //Displays the subtotal of Grocery order
        Console.WriteLine($"Shipping Cost: ${shippingCost}"); //Displays the shipping cost line
        Console.WriteLine($"Tax ({taxRate * 100}%): ${tax}"); //Displays the total tax due
        Console.WriteLine($"Order Total: ${orderTotal}"); // Displays users order total
    }
    //Method 6
    static void ThankYouMessage(Customer customer)
    {
        Console.WriteLine($"\nThank you, please come again, {customer.FirstName} {customer.LastName}!"); //This code displays a Thank You message with the customers first and last name
        Console.WriteLine("Your order has been placed successfully."); // This line of code lets the user know that their order has successfully been placed :)
    }
}