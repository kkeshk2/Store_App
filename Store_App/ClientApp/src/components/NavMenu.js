import React, { useState } from 'react';
import { Collapse, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';
import './NavMenu.css';


function NavMenu() {
    const [collapsed, setCollapsed] = useState(true);

    const toggleNavbar = () => {
        setCollapsed(!collapsed);
    };

    const HandleLogOut = () => {
        localStorage.removeItem("authtoken")
        window.location.reload()
    }

    const HandleVerify = async event => {
        if (localStorage.getItem("authtoken")) {
            try {
                const headers = { 'Authorization': "Bearer " + localStorage.getItem("authtoken") }
                const response = await fetch(`api/account/verifyaccount`, { headers });
                if (!response.ok) {
                    localStorage.removeItem("authtoken")
                    window.location.reload()
                }
            } catch (Exception) {
                localStorage.removeItem("authtoken")
                window.location.reload()
            }
        }
    }

    HandleVerify();

    if (localStorage.getItem("authtoken") != null) {
        return (
            <header>
                <Navbar className="navbar-expand-sm navbar-toggleable-sm navbar-light bg-white border-bottom box-shadow mb-3" expand="md">
                    <NavbarBrand tag={Link} to="/">Digital Domain</NavbarBrand>
                    <NavbarToggler onClick={toggleNavbar} className="mr-2" />
                    <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={!collapsed} navbar>
                        <ul className="navbar-nav flex-grow">
                            <NavItem>
                                <NavLink tag={Link} className="text-dark" to="/cart">Cart</NavLink>
                            </NavItem>
                            <NavItem>
                                <NavLink tag={Link} className="text-dark" to="/account">Account</NavLink>
                            </NavItem>
                            <NavItem>
                                <NavLink tag={Link} className="text-dark" to="/invoice-list">Invoices</NavLink>
                            </NavItem>
                            <NavItem>
                                <NavLink tag={Link} className="text-dark" onClick={HandleLogOut} to="/">Log Out</NavLink>
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
                            <NavLink tag={Link} className="text-dark" to="/create-account">Create Account</NavLink>
                        </NavItem>
                    </ul>
                </Collapse>
            </Navbar>
        </header>
    );
}

export default NavMenu;
