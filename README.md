## Installation

### 1. Prerequisites

- **.NET 9.0 SDK or Runtime**  
  Install from https://dotnet.microsoft.com/download  
- **Unix-like shell** (Linux, macOS, WSL)  
- **MongoDB connection string**  
  You’ll need a valid URI for your database (e.g. `mongodb://username:password@host:port/dbname`).  

### 2. Clone the repository

```bash
git clone https://github.com/hoffmann2109/todoCLI.git
cd todoCLI
````

### 3. Publish the application

From the repository root:

```bash
cd CLI_TODO
dotnet publish --configuration Release --output ./bin/Release/net9.0/publish
```

This will produce a folder at `CLI_TODO/bin/Release/net9.0/publish`.
### 4. Configure your environment

#### a. Create your `.env`

In the publish folder:

```bash
cat <<EOF > CLI_TODO/bin/Release/net9.0/publish/.env
MONGO_CONN=mongodb://username:password@host:port/dbname
EOF
```

> **Note:** the app will throw an error if `MONGO_CONN` is missing or invalid.

#### b. (Optional) Permanent shell export

If you’d rather not use a `.env`, add this line to `~/.bashrc` or `~/.zshrc`:

```bash
export MONGO_CONN="mongodb://username:password@host:port/dbname"
```

Then reload your shell:

```bash
source ~/.bashrc  # or ~/.zshrc
```

### 5. Install the launcher script

We’ve included a `todo` script in the repo so you can run the app with one word.

1. Ensure you have a `~/bin` directory in your `$PATH`:

   ```bash
   mkdir -p ~/bin
   echo 'export PATH="$HOME/bin:$PATH"' >> ~/.bashrc
   source ~/.bashrc
   ```

2. Copy the `todo` script into `~/bin` and make it executable:

   ```bash
   cp todo ~/bin/todo
   chmod +x ~/bin/todo
   ```

### 6. Usage

Now from **any** directory you can run:

```bash
todo # launches the app
```

---

## Important Notes

* **MongoDB access**: the app reads `MONGO_CONN` at runtime, so you **must** have network access and valid credentials for the database you specify.
* If you don’t control that MongoDB instance, the app will fail to connect—so out of the box it will only work for whoever owns the database.
* To use your **own** MongoDB, simply point `MONGO_CONN` at your instance (e.g. a local or cloud-hosted database).

```
