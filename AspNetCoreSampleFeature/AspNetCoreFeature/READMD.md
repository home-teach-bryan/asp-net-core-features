## 在Azure上取得佈署憑證

```
az ad sp create-for-rbac --name "asp-net-core-feature" --role contributor --scopes /subscriptions/8b7b8e94-876d-4222-9f1c-dc74baaf2b06/resourceGroups/asp-net-core/providers/Microsoft.Web/sites/asp-net-core-feature --json-auth
```

```
az ad sp create-for-rbac --name "myApp" --role contributor --scopes /subscriptions/<subscription-id>/resourceGroups/<group-name>/providers/Microsoft.Web/sites/<app-name> --json-auth
```


## 取得佈署憑證後加入GitHub Action中的Secret給Workflow使用

