import React, { useEffect, useState } from 'react';
import '../custom.css'; 

function Home() {
    const [products, setProducts] = useState([]);
    const [loading, setLoading] = useState(true);

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
        <div key={product.productSKU} className="product-card">
          <img src={"/emptyImage.jpeg"} alt={product.productName} />
          <h3>{product.productName}</h3>
          <p>{product.productDescription}</p>
          <p>Price: ${product.productPrice.toFixed(2)}</p>
        </div>
      ))}
    </div>

         
        </div>
    );
}

export default Home;
