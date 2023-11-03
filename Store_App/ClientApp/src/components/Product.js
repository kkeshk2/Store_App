import React, { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';


function Product() {
  const [product, setProduct] = useState(null);
  const {id: productId} = useParams();

  useEffect(() => {
    fetch(`/api/product/getone/${productId}`) 
      .then((response) => {
        console.log(productId);
        console.log("TEST\n");
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
		<h1>PRODUCT {productId}</h1>
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
