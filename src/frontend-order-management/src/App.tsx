import { useState, useEffect } from 'react'
import BurbLogo from './assets/burp_logo.png'
import './App.css'
import {MenuItems} from './components/MenuItems'
import {IOrder, getRecipes, IPostOrder, postOrder} from './services/backend'
import { IMenuItem, MenuItem } from './components/MenuItem'

function App() {
  const [count, setCount] = useState(0)
  const [orders, setOrders] = useState<IOrder[]>([])
  const [order, setOrder] = useState<IMenuItem | undefined>()
  
  const addToCart = (item: IMenuItem) => {
    setOrder(item)
  }


  const getData = async () => {
    const response = await getRecipes();
    const data: IOrder[]= response.data;
    console.log(data);
    setOrders(data);
  };
  useEffect(() => {
    getData()
  }, []);

  return (
    <>
      <div>
        <img src={BurbLogo} className="logo" alt="Burb logo" />
      </div>
      <h1>Burp 4.0</h1>
      <h2>Menu</h2>
      <div className="card">
        <MenuItems addToCart={addToCart}/>
      </div>
      <h2>Order</h2>
      <div className="card">
        <ul>
          {order ? <MenuItem item={order}/>: undefined}
        </ul>
        <button onClick={() =>{
          if(order){
            postOrder({name: order.name})
            setOrder(undefined)
          }
        } }><p>Confirm Order</p></button>
      </div>
    </>
  )
}

export default App
