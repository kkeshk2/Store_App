import React, { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';
import '../custom.css'; 



function Product() {
  const [product, setProduct] = useState(null);
  const [loading, setLoading] = useState(true);
  const { id: productId } = useParams();
  const [selectedQuantity, setSelectedQuantity] = useState(1);
  const [addedToCart, setAddedToCart] = useState(false);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await fetch(`api/product/getone?prodID=${productId}`);
        if (!response.ok) {
          throw new Error('Network response was not ok');
        }

        const data = await response.json();
        console.log('NETWORK GOOD');
        console.log(data);
        setProduct(data);
      } catch (error) {
        console.error('Error fetching product:', error);
      } finally {
        setLoading(false);
      }
    };

    fetchData();
  }, [productId]);

  const addToCart = async() => {
    // Add logic to handle adding the product to the cart
    console.log("URL:", `api/cart/addtocart?accountId=${2}&productId=${productId}&quantity=${selectedQuantity}`);

    try {
        setLoading(true);
        const response = await fetch(`api/cart/addtocart?accountId=${1}&productId=${productId}&quantity=${selectedQuantity}`);
        if (!response.ok) {
            throw new Error('Network response was not ok');
        }

        const data = await response.json();
        
        console.log('NETWORK GOOD');
        console.log(data);
        setAddedToCart(true);
    } catch (error) {
        console.error('Error adding to cart:', error);
    } finally{
      setLoading(false);
    }
    console.log(`Product ${product.ProductName} added to the cart`);
    // You can dispatch an action or perform other actions here
  };

  // if (loading) {
  //   return <p>Loading...</p>;
  // }

  if (!product) {
    return <p>Error loading product data</p>;
  }

    return (
     <div style={{ maxWidth: '800px', margin: 'auto' }}>
      {addedToCart &&
        <div style={{ backgroundColor: 'green', color: 'white', padding: '10px', textAlign: 'center' }}>
            Added to cart!
        </div>
      }
      <table style={{ width: '100%', marginTop: '5px', borderCollapse: 'collapse' }}>
        <tbody>
          <tr>
            <td colSpan={2}>
              <img
                src={product.ProductImageLocation || "/emptyImage.jpeg"}
                alt="Product Image"
                style={{ width: '100%', height: 'auto' }}
              />
            </td>
          </tr>
          <tr>
            <td style={{ textAlign: 'center', padding: '10px', backgroundColor: '#f5f5f5' }} colSpan={2}>
              <h1>{product.ProductName}</h1>
            </td>
          </tr>
          <tr>
            <td style={{ padding: '10px', backgroundColor: '#f5f5f5' }}>
              <strong>Price</strong>
            </td>
            <td style={{ padding: '10px', backgroundColor: '#f5f5f5' }}>
              {product.ProductPrice && `$${product.ProductPrice.toFixed(2)}`}
            </td>
          </tr>
          <tr>
            <td style={{ padding: '10px' }}>
              <strong>Manufacturer</strong>
            </td>
            <td style={{ padding: '10px' }}>
              {product.ProductManufacturer}
            </td>
          </tr>
          <tr>
            <td style={{ padding: '10px', backgroundColor: '#f5f5f5' }}>
              <strong>Rating</strong>
            </td>
            <td style={{ padding: '10px', backgroundColor: '#f5f5f5' }}>
              {product.ProductRating}
            </td>
          </tr>
          <tr>
            <td style={{ padding: '10px' }}>
              <strong>Description</strong>
            </td>
            <td style={{ padding: '10px' }}>
              {product.ProductDescription}
            </td>
          </tr>
          <tr>
            <td style={{ padding: '10px', backgroundColor: '#f5f5f5' }}>
              <strong>Category</strong>
            </td>
            <td style={{ padding: '10px', backgroundColor: '#f5f5f5' }}>
              {product.ProductCategory}
            </td>
          </tr>
          <tr>
            <td style={{ padding: '10px' }}>
              <strong>Dimensions</strong>
            </td>
            <td style={{ padding: '10px' }}>
              {product.ProductLength && product.ProductWidth && product.ProductHeight && (
                `${product.ProductLength} x ${product.ProductWidth} x ${product.ProductHeight} (inches)`
              )}
            </td>
          </tr>
          <tr>
            <td style={{ padding: '10px', backgroundColor: '#f5f5f5' }}>
              <strong>Weight</strong>
            </td>
            <td style={{ padding: '10px', backgroundColor: '#f5f5f5' }}>
              {product.ProductWeight && `${product.ProductWeight} lbs`}
            </td>
          </tr>
          <tr>
            <td style={{ padding: '10px' }}>
              <strong>SKU</strong>
            </td>
            <td style={{ padding: '10px' }}>
              {product.ProductSKU}
            </td>
          </tr>
          <tr>
            <td colSpan={2} style={{ textAlign: 'left', padding: '10px' }}>
              <button onClick={addToCart} className="btn-primary" disabled={loading} style={{ marginRight: '10px'}}>Add to Cart</button>
              <label htmlFor="quantity" style={{ marginRight: '10px'}}>Quantity:</label>
              <select
                id="quantity"
                name="quantity"
                value={selectedQuantity}
                onChange={(e) => setSelectedQuantity(parseInt(e.target.value))}
                style={{
                  padding: '10px',
                  fontSize: '16px',
                  borderRadius: '5px',
                  border: '1px solid #ccc',
                  backgroundColor: '#fff',
                  cursor: 'pointer',
                }}
              >
                {[1, 2, 3, 4, 5].map((number) => (
                  <option key={number} value={number}>
                    {number}
                  </option>
                ))}
              </select>
            </td>
            
          </tr>
        </tbody>
      </table>
    </div>
  );
}

export default Product;
