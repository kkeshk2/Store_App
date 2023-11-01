import React, { useState, useEffect } from 'react';

function Product({ productId }) {
  const [product, setProduct] = useState(null);

  useEffect(() => {
    // Replace this with your actual API call
    fetch(`/api/product/getone/${productId}`) 
      .then((response) => {
		response.json();
		console.log(response.json());
	})
      .then((data) => {
		console.log(data);
        setProduct(data);
      })
      .catch((error) => {
        console.error('Error fetching product:', error);
      });
  }, []);

  return (
    <div>
		<h1>PRODUCT</h1>
      {product ? (
        <div>
          <h1>{product.ProductName}</h1>
          <p>Price: ${product.ProductPrice}</p>
          {/* Add more product details as needed */}
        </div>
      ) : (
        <p>Loading...</p>
      )}
    </div>
  );
}

export default Product;
