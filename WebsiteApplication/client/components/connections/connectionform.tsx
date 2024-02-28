import {Button, Card, CardBody, CardFooter, Input, Link, Tab, Tabs, Textarea, useDisclosure} from "@nextui-org/react";
import {DatabaseCards} from "@/components/connections/databasecards";
import React from "react";
import {Key} from "@react-types/shared";
import {parseDatabaseAndServer} from "@/lib/utils";


export const ConnectionForm = () => {
    const [selectedTab, setSelectedTab] = React.useState<Key>("host");
    const [databaseType, setDatabaseType] = React.useState<string>("")
    const [port, setPort] = React.useState<string>("1234"); // Add state for port number
    const [databaseName, setDatabaseName] = React.useState<string>("")
    const [host, setHost] = React.useState<string>("")
    const [url, setUrl] = React.useState("");

    const handleUrlChange = (value:string) => {
        setUrl(value);
        const {database, server} = parseDatabaseAndServer(value)
        setDatabaseName(database)
        setHost(server)
    }

    const updatePort = (databaseType: string) => {
        switch (databaseType) {
            case "MYSQL":
                setPort("3306");
                break;
            case "POSTGRES":
                setPort("5432");
                break;
            case "MSSQL":
                setPort("1433");
                break;
            case "ORACLE":
                setPort("1521");
                break;
            default:
                setPort("1234");
                break;
        }
    };
    return (
        <div className="flex flex-col">
            <Card radius={"none"} shadow={"none"} className="max-w-full w-[440px] h-[560px]">
                <CardBody className="overflow-hidden">
                    <Tabs
                        fullWidth
                        variant="bordered"
                        size="sm"
                        aria-label="Tabs form"
                        selectedKey={selectedTab}
                        onSelectionChange={setSelectedTab}
                    >

                        <Tab key="host" title="Host">
                            <form className="flex flex-col gap-3">
                                <DatabaseCards
                                    onDatabaseSelect={selectedDatabase => {
                                        setDatabaseType(selectedDatabase);
                                        updatePort(selectedDatabase); // Update port based on selected database
                                    }}
                                    imageSizeClasses={"h-10 w-10"}
                                    baseCardClasses={"size-24"}/>
                                <div className={"flex flex-row gap-2"}>
                                    <Input
                                        className={"flex-2"}
                                        size="md"
                                        isRequired
                                        label="Database"
                                        placeholder="Enter your database name eg. master"
                                        type="text"
                                    />
                                    <Input
                                        className={"flex-1"}
                                        isRequired
                                        label="Port"
                                        placeholder={`eg. ${port}`}
                                        type="text"
                                    />
                                </div>
                                <div className={"flex"}>
                                    <Input
                                        className={"flex-1"}
                                        size="md"
                                        isRequired
                                        label="Host"
                                        placeholder="Enter your host eg. example-db.jettydash.com"
                                        type="text"
                                    />
                                </div>
                                <div className={"flex flex-row gap-2"}>
                                    <Input
                                        className={"flex-1"}
                                        isRequired
                                        label="Username"
                                        placeholder="Enter your username"
                                        type="text"/>
                                    <Input
                                        className={"flex-1"}
                                        isRequired
                                        label="Password"
                                        placeholder="Enter your password"
                                        type="password"
                                    />
                                </div>

                                <p className="text-center text-small ">
                                    Need a connection example?{" "}
                                    <Link size="sm" onPress={() => setSelectedTab("Url")}>
                                        Learn More
                                    </Link>
                                </p>

                            </form>
                        </Tab>
                        <Tab key="url" title="Url">
                            <form className="flex flex-col gap-3">
                                <DatabaseCards
                                    onDatabaseSelect={selectedDatabase => {
                                        setDatabaseType(selectedDatabase);
                                        updatePort(selectedDatabase); // Update port based on selected database
                                    }}
                                    imageSizeClasses={"h-10 w-10"}
                                    baseCardClasses={"size-24"}/>

                                <div className={"flex flex-col h-[124px]"}>
                                    <Textarea
                                        rows={3}
                                        isRequired
                                        label="Url"
                                        value={url}
                                        onValueChange={(value: string) => handleUrlChange(value)}
                                        labelPlacement="outside"
                                        placeholder="Enter your url eg. Server=myServerAddress;Port=1234;Database=myDataBase;Uid=myUsername;Pwd=myPassword;"
                                        className="flex-1"
                                    />
                                    <p className="text-default-500 text-small"><b>Database:</b> {databaseName ?? "null"} <b>Host:</b> {host ?? "null"}</p>
                                </div>

                                <div className={"flex flex-row gap-2"}>
                                    <Input
                                        className={"flex-1"}
                                        isRequired
                                        label="Username"
                                        placeholder="Enter your username"
                                        type="text"/>
                                    <Input
                                        className={"flex-1"}
                                        isRequired
                                        label="Password"
                                        placeholder="Enter your password"
                                        type="password"
                                    />
                                </div>

                                <p className="text-center text-small ">
                                    Need a connection example?{" "}
                                    <Link size="sm" onPress={() => setSelectedTab("Host")}>
                                        Learn More
                                    </Link>
                                </p>

                            </form>
                        </Tab>
                        {/*<Tab key="url" title="Url">*/}
                        {/*    <form className="flex flex-col gap-4 h-[300px]">*/}
                        {/*        <Input isRequired label="Name" placeholder="Enter your names" type="password"/>*/}
                        {/*        <Input isRequired label="Email" placeholder="Enter your email" type="email"/>*/}
                        {/*        <Input*/}
                        {/*            isRequired*/}
                        {/*            label="Password"*/}
                        {/*            placeholder="Enter your password"*/}
                        {/*            type="password"*/}
                        {/*        />*/}
                        {/*        <p className="text-center text-small">*/}
                        {/*            Already have an account?{" "}*/}
                        {/*            <Link size="sm" onPress={() => setSelectedTab("host")}>*/}
                        {/*                Host*/}
                        {/*            </Link>*/}
                        {/*        </p>*/}

                            {/*</form>*/}
                        {/*</Tab>*/}
                    </Tabs>
                </CardBody>

            </Card>
        </div>
    );
}