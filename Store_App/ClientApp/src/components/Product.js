import React, { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';


function Product() {
  const [product, setProduct] = useState(null);
  const {id: productId} = useParams();
  useEffect(() => {
	fetch(`api/product/getone?prodID=${productId}`)
	  .then((response) => {
		if (!response.ok) {
		  throw new Error('Network response was not ok');
		}
		console.log("NETWORK GOOD");
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
