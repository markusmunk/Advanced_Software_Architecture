import { IMenuItem, MenuItem } from './MenuItem'
import testMenu from '../assets/test-menu.json'
export function MenuItems ({addToCart}) {
    const items = testMenu.map((value: IMenuItem, index: number) => 
    <li key={index}>
        <MenuItem addToCart={addToCart} item={value} />
    </li>
    )
    return (
        <ul> 
            {items}
        </ul>
    )
}