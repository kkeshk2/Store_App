import React, { useEffect, useState } from 'react';
import '../custom.css';
import { Link } from 'react-router-dom';

import {
    Alert,
    Input,
    Label,
    Form,
    FormGroup,
    FormText,
    FormFeedback,
    Row,
    Col,
    Button,
    Dropdown,
    Nav,
    NavItem,
    NavLink,
    Card,
    CardBody,
    CardImg,
    CardTitle,
    CardSubtitle
} from 'reactstrap'

function Home() {
    const [products, setProducts] = useState([]);
    const [loading, setLoading] = useState(true);
    const [selection, setSelection] = useState("")
    const [search, setSearch] = useState("")
    const [isChecked, setIsChecked] = useState(false);

    useEffect(() => {
        const populateProducts = async () => {
            try {
                const response = await fetch('api/product/accessproductlist');
                const data = await response.json();
                setProducts(data.Products);

            }
            catch (error) {
                console.error('Error fetching data:', error);
            }
        };
        populateProducts();
    }, []);

    const handleSelection = (event) => {
        setSelection(event.target.value)
    }

    const handleSearch = (event) => {
        setSearch(event.target.value)
    }

    const handleCheck = (event) => {
        setIsChecked(!isChecked)
    }

    return (
        <div>
            <div className="d-flex flex-wrap justify-content-center align-items-center">
                <Form style={{ margin: "10px", maxWidth: "40rem", width: "40rem" }}>
                    <Row>
                        <Col xs="3">
                            <Input type="select" size="sm" onChange={e => handleSelection(e)}>
                                <option value="" selected>Choose Category</option>
                                <option value="Graphics Card">Graphics Cards</option>
                                <option value="Processor">Processors</option>
                                <option value="Cooling">Cooling</option>
                            </Input>

                        </Col>
                        <Col xs="6">
                            <Input placeholder="Search" size="sm" onChange={e => handleSearch(e)}>
                            </Input>
                        </Col>
                        <Col xs="3">
                            <Input id="checkbox" type="checkbox" style={{ marginRight: "1em" }} checked={isChecked} onChange={()=>handleCheck()}>
                                On Sale
                            </Input>
                            <Label for="checkbox">
                                {` `}On Sale
                            </Label>
                        </Col>
                    </Row>
                </Form>
            </div>
            <div className="d-flex flex-wrap justify-content-center">
                {products.map((product) => (
                    <Card style={{ margin: "10px", maxWidth: "24rem", width: "24rem" }}
                        hidden={(selection != "" && selection.toLowerCase() != product.ProductCategory.toLowerCase()) ||
                            (search != "" && (!product.ProductName.toLowerCase().includes(search.toLowerCase()))) ||
                            (isChecked && product.ProductSale == 0)
                        }
                    >
                        <CardBody>
                            <CardSubtitle style={{ fontSize: 20, fontWeight: 600 }}>
                                {product.ProductName}
                            </CardSubtitle>
                            <Row>
                                <Col>
                                    <CardSubtitle tag="h6">
                                        By {product.ProductManufacturer}
                                    </CardSubtitle>
                                </Col>
                                <Col style={{ textAlign: "right" }}>
                                    <CardSubtitle tag="h6">
                                        {'\u2605'}{product.ProductRating}
                                    </CardSubtitle>
                                </Col>
                            </Row>
                            <br></br>
                            <CardImg
                                alt={product.ProductName}
                                src={product.ProductImageLocation}
                                height="62.5%"
                            />
                            <br></br><br></br>
                            <Row>
                                <Col>
                                    <CardSubtitle tag="h4" hidden={product.ProductSale !== 0}>
                                        ${product.ProductPrice}
                                    </CardSubtitle>
                                    <CardSubtitle tag="h4" hidden={product.ProductSale === 0}>
                                        <s style={{ color: "darkred" }}>${product.ProductPrice}</s> ${product.ProductPrice - product.ProductSale}
                                    </CardSubtitle>
                                </Col>
                            </Row>
                        </CardBody>
                    </Card>
                ))}
            </div>
        </div>
    );
}

export default Home;
