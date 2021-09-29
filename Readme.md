# MassTransit example using custom direct RabbitMQ exchanges


```

DummyApiService
    ↓
    (my-routing-key + correlation_id + reply_to)
    ↓
RabbitMQ(my-exchange)
    ↓
pythonWorker
    ↓
    (reply_to + correlation_id)
    ↓
RabbitMQ(default-exchange)

```

Install RabbitMQ

```Shell
helm repo add bitnami https://charts.bitnami.com/bitnami
helm repo update
helm install test --set auth.username=root,auth.password=root,auth.erlangCookie=secretcookie bitnami/rabbitmq
helm delete test
```

forward ports:

```Shell
kubectl port-forward --namespace default svc/test-rabbitmq 15672:15672 5672:5672
```