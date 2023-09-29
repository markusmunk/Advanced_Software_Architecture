import axios from 'axios';

export interface IOrders {
    id: string;
    name: string;
}

const API_URL: string = 'http://localhost:5033';

export async function getRecipes(): Promise<IOrders[]> {
  return axios.get(`${API_URL}/api/orders`, {
    headers: {
        "Content-Type": "application/json"
        }
    });
}