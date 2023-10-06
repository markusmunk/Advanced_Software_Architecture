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