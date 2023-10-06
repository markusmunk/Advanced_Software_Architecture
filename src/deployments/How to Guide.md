# Frontend deployment

1. Apply the frontend-deployment.yaml file 

```
kubectl apply -f frontend-deployment.yaml
```

2. Check the status of the deployment

```
kubectl get all
```

3. Port forward the frontend service

```
kubectl port-forward service/frontend-order-management-service 5173:80
```

# Postgres deployment

1. Apply the postgres-deployment.yml file 

```
kubectl apply -f postgres-deployment.yml
```

2. Check the status of the deployment

```
kubectl get all
```

# Order management service deployment

1. Apply the order-management-deployment.yaml file 

```
kubectl apply -f order-management-deployment.yaml
```

2. Check the status of the deployment

```
kubectl get all
```

3. Port forward the order service

```
kubectl port-forward service/order-management-service 5033:80
```