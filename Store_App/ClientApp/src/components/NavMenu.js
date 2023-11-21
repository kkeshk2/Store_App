import React, { useState, useEffect } from 'react';
import { Collapse, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { Link, useNavigate } from 'react-router-dom';
import './NavMenu.css';


function NavMenu() {
    const [collapsed, setCollapsed] = useState(true);
    const navigate = useNavigate()

    const toggleNavbar = () => {
        setCollapsed(!collapsed);
    };

    const HandleLogOut = () => {
        localStorage.removeItem("authtoken")
        navigate("/")
    }

    if (localStorage.getItem("authtoken") != null) {
        return (
            <header>
                <Navbar className="navbar-expand-sm navbar-toggleable-sm navbar-light bg-white border-bottom box-shadow mb-3" expand="md">
                    <NavbarBrand tag={Link} to="/">Digital Domain</NavbarBrand>
                    <NavbarToggler onClick={toggleNavbar} className="mr-2" />
                    <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={!collapsed} navbar>
                        <ul className="navbar-nav flex-grow">
                            <NavItem>
                                <NavLink tag={Link} className="text-dark" onClick={HandleLogOut}>Log Out</NavLink>
                            </NavItem>
                            <NavItem>
                                <NavLink tag={Link} className="text-dark" to="/">Home</NavLink>
                            </NavItem>
                            <NavItem>
                                <NavLink tag={Link} className="text-dark" to="/cart">Cart</NavLink>
                            </NavItem>
                            <NavItem>
                                <NavLink tag={Link} className="text-dark" to="/checkout">Checkout</NavLink>
                            </NavItem>
                        </ul>
                    </Collapse>
                </Navbar>
            </header>
        );
    }

    return (
        <header>
            <Navbar className="navbar-expand-sm navbar-toggleable-sm navbar-light bg-white border-bottom box-shadow mb-3" expand="md">
                <NavbarBrand tag={Link} to="/">Digital Domain</NavbarBrand>
                <NavbarToggler onClick={toggleNavbar} className="mr-2" />
                <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={!collapsed} navbar>
                    <ul className="navbar-nav flex-grow">
                        <NavItem>
                            <NavLink tag={Link} className="text-dark" to="/login">Log In</NavLink>
                        </NavItem>
                        <NavItem>
                            <NavLink tag={Link} className="text-dark" to="/">Home</NavLink>
                        </NavItem>
                        <NavItem>
                            <NavLink tag={Link} className="text-dark" to="/cart">Cart</NavLink>
                        </NavItem>
                        <NavItem>
                            <NavLink tag={Link} className="text-dark" to="/checkout">Checkout</NavLink>
                        </NavItem>
                    </ul>
                </Collapse>
            </Navbar>
        </header>
    );
}

export default NavMenu;
