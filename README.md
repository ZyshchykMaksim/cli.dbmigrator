# cli.dbmigrator

**commands:**

  try-connect &nbsp; &nbsp;Check the database connection.

  migration   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Adds changes to the database.
 
  help &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Display more information on a specific command.

  version  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Display version information.

#**try-connect command:**

  -c, --connection &nbsp;Required. The database connection string.

  --help &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Display this help screen.

  --version &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Display version information.
  
```
 /* 
 * Example:
 * Run cmd.exe or PowerShell
 */ 
 
 cli.dbmigrator help
 cli.dbmigrator try-connect -c "Server=(localdb)\MSSQLLocalDB; Database=LD_TEST; Trusted_connection=true;"
```

#**migration:**

  -d, --directory &nbsp;&nbsp;&nbsp;Required. The path to directory where SQL scripts are
                       stored.

  -t, --transaction &nbsp;(Default: true) Allows to roll back changes if an error
                       occurs.

  -c, --connection     &nbsp;Required. The database connection string.

  --help &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Display this help screen.

  --version &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Display version information.

```
 /* 
 * Example:
 * Run cmd.exe or PowerShell
 */ 
 
 cli.dbmigrator help
 cli.dbmigrator migration -c "Server=(localdb)\MSSQLLocalDB; Database=LD_TEST; Trusted_connection=true;" -d "C:\SQL_SCRIPTS"
```
