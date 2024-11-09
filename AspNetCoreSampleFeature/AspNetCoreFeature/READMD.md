# 建置與佈署

## 建置前置作業 
- 先確認應用程式在本地IDE建置與執行沒有問題
- 將專案加入Dockerfile，先在本地使用`docker build`確認可以建置出docker image
- (Optional) 在本地執行docker image，確認應用程式在container內執行沒有問題
- 在GitHub帳號上建立PAT(Personal Access Token)，給之後的GitHub Action Workflow使用，權限必須要選Package(Read,Write)

### GitHub Action Workflow
- 在專案的根目錄建立`.github/workflow/{workflow}.yaml`
- 加入建置相關的Action
    - Git Checkout 簽出程式碼
        - (Optional) 設置.NET執行環境
        - (Optional) 建置專案
        - (Optional) 執行單元測試
    - 設定docker建置環境
    - 使用dockerfile封裝應用程式成image(docker build)
    - 登入儲存庫(docker login)
        - 公有儲存庫(ex. Docker Hub, Azure container registry, Google Artifact Registry, Github Package...etc)
    - 推送image(docker push)

## 佈署前置作業
### Azure App Service
- 先建立Azure App Service
    - 佈署方式選擇容器
    - 先選擇快速入門的映像檔
- 設定應用程式環境變數(for docker)
    - 設定`DOCKER_REGISTRY_SERVER_URL`為docker儲存庫位置 ex. https://gcr.io
    - 設定`DOCKER_REGISTRY_USERNAME`為docker儲存庫登入的使用者
    - 設定`DOCKER_REGISTRY_PASSWORD`為docker儲存庫登入的密碼(GitHub帳號設定的PAT)
- 設定應用程式環境變數(for ASP.NET Core)
    - 給程式使用的 例如: `ASPNETCORE_ENVIRONMENT=Development`)
- 在Azure上取得登入憑證，並加入GitHub Action中的Secret給Workflow使用
```
az ad sp create-for-rbac --name "myApp" --role contributor --scopes /subscriptions/<subscription-id>/resourceGroups/<group-name>/providers/Microsoft.Web/sites/<app-name> --json-auth
```


### GitHub Action Workflow
- 登入Azure (使用先設定於Secrets中的登入憑證)
- 佈署App Service (推送image到App Service中)
- 登出Azure


# 透過Jenkins佈署IIS on Windows

## Web Server

1. 安裝IIS
2. 安裝WebDeploy (With Remote Agent)
3. 安裝ASP.NET Core Hosting Bundle(執行asp.net core的runtime環境) 

## Jenkins
1. 安裝Jenkins管理介面
2. 安裝JDK 17 以上(for Jenkins使用)
3. 安裝Vistual Studio Build Tools 
4. 安裝.NET Core SDK
5. 安裝Git
6. 安裝Web Deploy
