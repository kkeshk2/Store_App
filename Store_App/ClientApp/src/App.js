import React from 'react';
import { Route, Routes } from 'react-router-dom';
import Layout from './components/Layout';
import Home from './components/Home';
import Product from './components/Product';
import Cart from './components/Cart';
import Login from './components/Login'
import Checkout from './components/Checkout';
import CreateAccount from './components/CreateAccount'
import Account from './components/Account'
import InvoiceList from './components/InvoiceList'
import Invoice from './components/Invoice'
import Unauthorized from './components/Unauthorized'
import NotFound from './components/NotFound'
import ServerError from './components/ServerError'


function App() {
    return (
        <Layout>
            <Routes>
                <Route path="/" element={<Home />} />
                <Route path="/product/:id" element={<Product />} />
                <Route path="/cart" element={<Cart />} />
                <Route path="/login" element={<Login />} />
                <Route path="/checkout" element={<Checkout />} />
                <Route path="/create-account" element={<CreateAccount />} />
                <Route path="/account" element={<Account />} />
                <Route path="/invoice-list" element={<InvoiceList />} />
                <Route path="/invoice/:id" element={<Invoice />} />
                <Route path="/unauthorized" element={<Unauthorized />} />
                <Route path="/not-found" element={<NotFound />} />
                <Route path="/server-error" element={<ServerError />} />
            </Routes>
        </Layout>
    );
}

export default App;
