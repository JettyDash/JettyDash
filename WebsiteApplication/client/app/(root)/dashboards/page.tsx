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
export default function Dashboards({}) {
    const [selected, setSelected] = React.useState<Key>("login");

    return (
        <div className="flex flex-col w-full">
            <Card className="max-w-full w-[340px] h-[400px]">
                <CardBody className="overflow-hidden">
                    <Tabs
                        fullWidth
                        variant="bordered"
                        size="md"
                        aria-label="Tabs form"
                        selectedKey={selected}
                        onSelectionChange={setSelected}
                    >
                        <Tab key="login" title="Login">
                            <form className="flex flex-col gap-4">
                                <Input isRequired label="Email" placeholder="Enter your email" type="email"/>
                                <Input
                                    isRequired
                                    label="Password"
                                    placeholder="Enter your password"
                                    type="password"
                                />
                                <p className="text-center text-small">
                                    Need to create an account?{" "}
                                    <Link size="sm" onPress={() => setSelected("sign-up")}>
                                        Sign up
                                    </Link>
                                </p>
                                <div className="flex gap-2 justify-end">
                                    <Button fullWidth color="primary">
                                        Logins
                                    </Button>
                                </div>
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