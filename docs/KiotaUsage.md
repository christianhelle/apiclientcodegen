# Generating code using Microsoft Kiota

To generate code using Kiota, Right click on a project or location under a project and select **Add -> New REST API Client -> Generate with Microsoft Kiota**

![Add New REST API Client](/images/kiota/add-new.png)

This will prompt you for a URL to download the OpenAPI specifications file.

If the operation worked successfully then you should see a code-behind file for the OpenAPI specifications. Once you have generated code, all changes to the OpenAPI specifications file will trigger a re-generation of the code-behind file via the **KiotaCodeGenerator** custom tool

![Custom Tool Experience](/images/kiota/custom-tool-experience.png)

If you already have an OpenAPI specifications file in the project, then you can also generate code using Kiota by right clicking on the file then selecting **REST API Client Generator -> Generate with Microsoft Kiota**

![Generate code using Kiota](/images/kiota/generate-from-existing.png)

Kiota generates multiple files and a complex folder structure internally by default, and this extension's default behavior is to merge all files into a single code-behind file. It is possible to generate multiple files directly to the project folder by setting **Generate Multiple Files** to **True** in the Kiota settings, under Tools -> REST API Client Code Generator -> Kiota

![Kiota Options](/images/options-kiota.png)

An example output of generating multiple files looks something like this:

![Kiota multiple files output](/images/generate-kiota-output.png)

It's possible to run Kiota updates by right clicking on the `kiota-lock.json` configuration file and selecting **Generate Kiota output**. You can customize the Kiota configuration directly from the `kiota-lock.json` file