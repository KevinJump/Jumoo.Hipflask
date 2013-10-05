Jumoo.Hipflask
==============
Do you spend coutless seconds everytime you start an umbraco project wondering which version of the latest hip framework you want to work with ? 

Do you then spend at least a minute downloading it, and coping it to the right place in umbraco ? 

Well you're the type of Hoopy Frood who will love Hipflask

Hipflask is a trend installer, it provides a dashboard page that lets you download and install the latest frameworks.

Hipflask will deliver to your umbraco door:

- #Bootstrap
- Jquery
- Font-Awesome
- AngularJs
- famfamfamicons (for doctypes)

Note: this is just the fiels you need to bring your own funky

The science bit
===============

Bipflask has a config file ( config/hipflask.config ) that tells it where to download things, and where to copy them to. 

So when you ask it to, hipflask downloads the zip file, unzips it, then copies bits into your umbraco installation.

at the end you have the files in places they are meant to be (like css or scripts folders)

Hipflask is not NuGet
