import axios from 'axios';

export interface IOrder {
    id: string;
    name: string;
}
export interface IPostOrder {
    name: string;
}

const API_URL: string = 'http://order-management-service:80';

export async function getRecipes(): Promise<IOrder[]> {
  return axios.get(`${API_URL}/api/orders`, {
    headers: {
        "Content-Type": "application/json"
        }
    });
}

export async function postOrder(item: IPostOrder): Promise<IOrder[]> {
    return axios.post(`${API_URL}/api/orders`, item, {
      headers: {
          "Content-Type": "application/json"
          }
      });
  }