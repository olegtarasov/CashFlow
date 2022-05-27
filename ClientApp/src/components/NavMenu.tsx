import React, {useState} from 'react';
import {Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink} from 'reactstrap';
import {Link} from 'react-router-dom';

export function NavMenu() {
    const [isOpen, setIsOpen] = useState(false);

    return (
        <header>
            <Container>
                <Navbar className="navbar-expand-sm navbar-toggleable-sm border-bottom box-shadow mb-3" light>
                    <NavbarBrand tag={Link} to="/">CashFlow</NavbarBrand>
                    <NavbarToggler onClick={() => setIsOpen(!isOpen)} className="mr-2"/>
                    <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={isOpen}
                              navbar>
                        <ul className="navbar-nav flex-grow">
                            <NavItem>
                                <NavLink tag={Link} className="text-dark" to="/">Spending</NavLink>
                            </NavItem>
                        </ul>
                    </Collapse>
                </Navbar>
            </Container>
        </header>
    );
}
