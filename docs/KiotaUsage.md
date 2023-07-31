# Generating code using Microsoft Kiota

To generate code using Kiota, Right click on a project or location under a project and select **Add -> New REST API Client -> Generate with Microsoft Kiota**

![Add New REST API Client](/images/kiota/add-new.png)

This will prompt you for a URL to download the OpenAPI specifications file.

If the operation worked successfully then you should see a code-behind file for the OpenAPI specifications. Once you have generated code, all changes to the OpenAPI specifications file will trigger a re-generation of the code-behind file via the **KiotaCodeGenerator** custom tool

![Custom Tool Experience](/images/kiota/custom-tool-experience.png)

If you already have a OpenAPI specifications file in the project, then you can also generate code using Kiota by right clicking on the file then selecting **REST API Client Generator -> Generate with Microsoft Kiota**

![Generate code using Kiota](/images/kiota/generate-from-existing.png)