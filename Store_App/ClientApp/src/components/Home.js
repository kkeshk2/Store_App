import React, { useEffect, useState } from 'react';
import '../custom.css'; 
import { Link } from 'react-router-dom'; 

function Home() {
    const [products, setProducts] = useState([]);
    // const [loading, setLoading] = useState(true);

    useEffect(() => {
        const populateProducts = async()=>{
            try{
                const response = await fetch('api/product/getall');
                const data = await response.json();
                setProducts(data);
                
            }
            catch(error){
                console.error('Error fetching data:', error);
            }
        };
        populateProducts();
    }, []);
    return (
             
        <div>
             <div className="product-grid">
      {products.map((product) => (
        <Link to={`/product/${product.productId}`} key={product.productSKU} className="product-card">
           <img src={product.productImageLocation ?? "/emptyImage.jpeg"} alt={product.productName} />
  <h3 className="product-title">{product.productName}</h3>
  <p className="product-description">{product.productDescription}</p>
  <p className="product-price">Price: ${product.productPrice.toFixed(2)}</p>
        </Link>
      ))}
    </div>

         
        </div>
    );
}

export default Home;
