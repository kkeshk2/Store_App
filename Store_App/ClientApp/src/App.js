import React from 'react';
import { Route, Routes } from 'react-router-dom';
import Layout from './components/Layout';
import Home from './components/Home';
import {Counter} from './components/Counter';
import {FetchData} from './components/FetchData';
import Product from './components/Product';
import Cart from './components/Cart';
import Login from './components/Login'
import CreateAccount from './components/CreateAccount'


function App() {
    return (
        <Layout>
            <Routes>
                <Route path="/" element={<Home />} />
                <Route path="/counter" element={<Counter />} />
                <Route path="/product/:id" element={<Product />} />
                <Route path="/cart" element={<Cart />} />
                <Route path="/fetch-data" element={<FetchData />} />
                <Route path="/login" element={<Login />} />
                <Route path="/create-account" element={<CreateAccount />}/>
            </Routes>
        </Layout>
    );
}

export default App;
