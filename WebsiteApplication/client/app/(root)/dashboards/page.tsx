//
// import type {Metadata} from "next";
//
// export const metadata: Metadata = {
//     title: "Dashboards | JettyDash",
// };
//
// export default async function Dashboards({ }) {
//   return (
//     <div>
//       <h1>Dashboards</h1>
//     </div>
//   );
// }
//

'use client';
import {Tabs, Tab, Input, Link, Button, Card, CardBody, CardHeader} from "@nextui-org/react";
import React from "react";
import {Key} from "@react-types/shared";
import Image from "next/image";
import {DatabaseCards} from "@/components/connections/databasecards";

export default function Dashboards({}) {
    const [selected, setSelected] = React.useState<Key>("login");
    const [database, setDatabase] = React.useState<string>("")
    const [port, setPort] = React.useState<string>("1234"); // Add state for port number

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
        <div className="flex flex-col w-full">
            <Card className="max-w-full w-[440px] h-[700px]">
                <CardHeader>
                    <Image className={"dark:invert"} width={"32"} height={"32"} src={"../../../assets/icons/global.svg"}
                           alt={"global"}/>
                </CardHeader>
                <CardBody className="overflow-hidden">
                    <Tabs

                        fullWidth
                        variant="bordered"
                        size="sm"
                        aria-label="Tabs form"
                        selectedKey={selected}
                        onSelectionChange={setSelected}
                    >

                        <Tab key="login" title="Login">
                            <form className="flex flex-col gap-3">
                                <DatabaseCards
                                    onDatabaseSelect={selectedDatabase => {
                                        setDatabase(selectedDatabase);
                                        updatePort(selectedDatabase); // Update port based on selected database
                                    }}
                                    imageSizeClasses={"h-10 w-10"}
                                    baseCardClasses={"size-24"}/>
                                <div className={"flex flex-row gap-2"} >
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
                                <div className={"flex"} >
                                    <Input
                                        className={"flex-1"}
                                        size="md"
                                        isRequired
                                        label="Host"
                                        placeholder="Enter your host eg. example-db.jettydash.com"
                                        type="text"
                                    />
                                </div>
                                <div className={"flex flex-row gap-2"} >
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

                                <p className="text-center text-small">
                                    Need a connection example?{" "}
                                    <Link size="sm" onPress={() => setSelected("sign-up")}>
                                        Learn More
                                    </Link>
                                </p>

                            </form>
                        </Tab>
                        <Tab key="sign-up" title="Sign up">
                            <form className="flex flex-col gap-4 h-[300px]">
                                <Input isRequired label="Name" placeholder="Enter your names" type="password"/>
                                <Input isRequired label="Email" placeholder="Enter your email" type="email"/>
                                <Input
                                    isRequired
                                    label="Password"
                                    placeholder="Enter your password"
                                    type="password"
                                />
                                <p className="text-center text-small">
                                    Already have an account?{" "}
                                    <Link size="sm" onPress={() => setSelected("login")}>
                                        Login
                                    </Link>
                                </p>
                                <div className="flex gap-2 justify-end">
                                    <Button fullWidth color="primary">
                                        Sign up
                                    </Button>
                                </div>
                            </form>
                        </Tab>
                    </Tabs>
                </CardBody>
            </Card>
        </div>
    )
};