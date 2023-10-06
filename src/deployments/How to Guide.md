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

1. Install postgres with Helm

```
helm install postgresql --version=12.1.5 --set auth.username=admin --set auth.password=password --set auth.database=orders-db --set primary.extendedConfiguration="password_encryption=md5" --repo https://charts.bitnami.com/bitnami postgresql
```

2. Create secret

```
kubectl create secret generic postgres-credentials --from-literal=connectionString="Host=postgresql;Port=5432;Database=orders-db;Username=admin;Password=password;"
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
kubectl port-forward svc/order-management-service 5033:5033
```