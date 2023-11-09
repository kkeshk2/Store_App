import React from 'react';
import { Route, Routes } from 'react-router-dom';
import Layout from './components/Layout';
import Home from './components/Home';
import {Counter} from './components/Counter';
import {FetchData} from './components/FetchData';
import Product from './components/Product';
import Cart from './components/Cart';


function App() {
    return (
        <Layout>
            <Routes>
                <Route path="/" element={<Home />} />
                <Route path="/counter" element={<Counter />} />
                <Route path="/product/:id" element={<Product />} />
                <Route path="/cart/:id" element={<Cart />} />
                <Route path="/fetch-data" element={<FetchData />} />
            </Routes>
        </Layout>
    );
}

export default App;
