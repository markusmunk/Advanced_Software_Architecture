import {FiShoppingCart} from 'react-icons/fi'
import './MenuItem.css'
export interface IMenuItem {
    name: string;
    price: number;
    description: string;
    image: string;
}

export function MenuItem (props) {
    const {item}: IMenuItem = props;
    const addToCart = () => {
        props.addToCart(item)
    }
    return (
        <div className='item'> 
            <h3>{item.name}</h3>
            <p>{item.description}</p>
            <p>{item.price}</p>
            <img src={item.image} alt={item.name} />
            {props.addToCart ? <button className='btn' onClick={addToCart}><FiShoppingCart/></button> : undefined}
            
        </div>
    )
}