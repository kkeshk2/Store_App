import React, { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';


function Product() {
  const [product, setProduct] = useState(null);
  const {id: productId} = useParams();
  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await fetch(`api/product/getone?prodID=${productId}`);
        if (!response.ok) {
          throw new Error('Network response was not ok');
        }
  
        const data = await response.json(); // Parse the response as JSON
  
        console.log("NETWORK GOOD");
        console.log(data);
        setProduct(data); // Set the product state with the parsed data
      } catch (error) {
        console.error('Error fetching product:', error);
      }
    };
  
    fetchData();
  }, [productId]);
  
  

  return (
    <div>
      <h1>PRODUCT {productId}</h1>
      {product ? (
        <div>
          <img src="/emptyImage.jpeg" alt="Product Image" style={{ width: '100%', height: 'auto' }} />
          <h1>{product.productName}</h1>
          {product.productPrice && <p>Price: ${product.productPrice.toFixed(2)}</p>}
          {product.productManufacturer && <p>Manufacturer: {product.productManufacturer}</p>}
          {product.productRating && <p>Rating: {product.productRating}</p>}
          {product.productDescription && <p>Description: {product.productDescription}</p>}
          {product.productCategory && <p>Category: {product.productCategory}</p>}
          {product.productLength && product.productWidth && product.productHeight && (
            <p>Dimensions: {product.productLength}x{product.productWidth}x{product.productHeight} (inches)</p>
          )}
          {product.productWeight && <p>Weight: {product.productWeight} lbs</p>}
          {product.productSKU && <p>SKU: {product.productSKU}</p>}
        </div>
      ) : (
        <p>Loading...</p>
      )}
    </div>
  );
  
  
}

export default Product;
