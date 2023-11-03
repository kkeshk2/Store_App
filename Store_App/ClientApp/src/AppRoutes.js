import { Counter } from "./components/Counter";
import { FetchData } from "./components/FetchData";
import Product from './components/Product';

// WE WILL NOT BE USING THIS FORM OF ROUTES: CHECK APP.JS
import Home from './components/Home';
const AppRoutes = [
  {
    index: true,
    element: <Home />
  },
  {
    path: '/counter',
    element: <Counter />
  },
  {
    path: `/product/:id`,
    element: <Product/>
  },
  {
    path: '/fetch-data',
    element: <FetchData />
  }
];

export default AppRoutes;
