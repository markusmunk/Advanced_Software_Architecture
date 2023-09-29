import axios from 'axios';
interface IOrders {
    id: string;
    name: string;
  }
const defaultPosts:IOrders[] = [];
const API_URL = 'http://localhost:3001';

export async function getRecipes() {
  return axios.get(`${API_URL}/recipes`, {
    headers: {
        "Content-Type": "application/json"
        }
    });
}