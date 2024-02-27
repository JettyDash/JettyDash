import React from "react";
import {
    Modal,
    ModalContent,
    ModalHeader,
    ModalBody,
    ModalFooter,
    Button,
    useDisclosure,
    Checkbox,
    Input,
    Link,
    Card, CardBody, Tabs, Tab, RadioGroup, Radio
} from "@nextui-org/react";
import {PlusIcon} from "@/components/icons/PlusIcon";
import {Key} from "@react-types/shared";
import {AnchorIcon, EditIcon} from "@nextui-org/shared-icons";
import {Label} from "@radix-ui/react-menu";
// import {DatabaseCards} from "@/components/connections/databasecards";


export const CreateNewDatabase = () => {
    const {isOpen, onOpen, onOpenChange} = useDisclosure();
    const [selected, setSelected] = React.useState<Key>("login");

    return (
        <>
            <Button
                onPress={onOpen}
                className="bg-foreground text-background"
                endContent={<PlusIcon/>}
                size="sm"
            >
                Add New
            </Button>
            <Modal
                isOpen={isOpen}
                onOpenChange={onOpenChange}
                placement="top-center"
            >
                <ModalContent className="flex items-center bg-pink-600 w-[800px] h-[800px]">
                    {(onClose) => (
                        <>
                            <ModalHeader className="flex flex-col gap-1">New Connection</ModalHeader>
                            <ModalBody className="bg-cyan-500 w-full h-full">

                                    <Tabs
                                        color="default"
                                        className="overflow-hidden w-full h-full" /*w-[340px] h-[400px] */
                                        fullWidth
                                        size="sm"
                                        aria-label="Tabs form"
                                        selectedKey={selected}
                                        onSelectionChange={setSelected}
                                    >
                                        <Tab key="login" title="Login" >
                                            <form className="grid grid-cols-8 gap-3 w-full grid-rows-4">


                                                {/*<div className="col-span-2">1</div>*/}
                                                {/*<div className="col-span-2 col-start-3">2</div>*/}
                                                {/*<div className="col-span-2 col-start-5">3</div>*/}
                                                {/*<div className="col-span-2 col-start-7">4</div>*/}
                                                {/*<div className="col-span-6 row-start-2">5</div>*/}
                                                {/*<div className="col-span-8 col-start-1 row-start-3">6</div>*/}
                                                {/*<div className="col-span-2 col-start-7 row-start-2">7</div>*/}
                                                {/*<div className="col-span-4 row-start-4">8</div>*/}
                                                {/*<div className="col-span-4 col-start-5 row-start-4">9</div>*/}

                                                    {/*<div className="col-span-2">1</div>*/}
                                                    {/*<div className="col-span-2 col-start-3">2</div>*/}
                                                    {/*<div className="col-span-2 col-start-5">3</div>*/}
                                                    {/*<div className="col-span-2 col-start-7">4</div>*/}
                                                <div id="cards" className="col-span-8 h-20">
                                                    {/*<DatabaseCards/>*/}
                                                </div>


                                                <Input
                                                    className="h-5 col-span-6 row-start-2"
                                                    variant="bordered"
                                                    size="sm"
                                                    isRequired
                                                    label="Database"
                                                    labelPlacement="outside"
                                                    type="text"
                                                />
                                                <Input
                                                    className="h-5 col-span-8 col-start-1 row-start-3"
                                                    variant="bordered"
                                                    size="sm"
                                                    isRequired
                                                    label="Host"
                                                    labelPlacement="outside"
                                                    type="text"
                                                />
                                                <Input
                                                    className="h-5 col-span-2 col-start-7 row-start-2"
                                                    variant="bordered"
                                                    size="sm"
                                                    isRequired
                                                    label="Port"
                                                    labelPlacement="outside"
                                                    type="numeric"
                                                />
                                                <Input
                                                    className="h-5 col-span-4 row-start-4"
                                                    variant="bordered"
                                                    size="sm"
                                                    isRequired
                                                    label="Username"
                                                    labelPlacement="outside"
                                                    type="username"
                                                />
                                                <Input
                                                    className="h-5 col-span-4 col-start-5 row-start-4"
                                                    variant="bordered"
                                                    size="sm"
                                                    isRequired
                                                    label="Password"
                                                    labelPlacement="outside"
                                                    type="password"
                                                />
                                            </form>
                                        </Tab>
                                        <Tab key="sign-up" title="Sign up">
                                            <form className="flex flex-col gap-2 w-[250px] h-[250px]">
                                                <Input isRequired label="Name" placeholder="Enter your name"
                                                       type="password"/>
                                                <Input isRequired label="Email" placeholder="Enter your email"
                                                       type="email"/>
                                                <Input
                                                    isRequired
                                                    label="Password"
                                                    placeholder="Enter your password"
                                                    type="password"
                                                />
                                                <Link
                                                    className="text-center text-small text-blue-800 text-weight-bold"

                                                    isExternal
                                                    showAnchorIcon
                                                    href="/docs"
                                                    anchorIcon={<AnchorIcon />}
                                                >
                                                    Need Reference?{"  "}
                                                </Link>

                                            </form>
                                        </Tab>
                                    </Tabs>
                                {/*<Input*/}
                                {/*    autoFocus*/}
                                {/*    endContent={*/}
                                {/*        "ssdfsdf"*/}
                                {/*    }*/}
                                {/*    label="Email"*/}
                                {/*    placeholder="Enter your email"*/}
                                {/*    variant="bordered"*/}
                                {/*/>*/}
                                {/*<Input*/}
                                {/*    endContent={*/}
                                {/*        "5454"*/}
                                {/*    }*/}
                                {/*    label="Password"*/}
                                {/*    placeholder="Enter your password"*/}
                                {/*    type="password"*/}
                                {/*    variant="bordered"*/}
                                {/*/>*/}
                                {/*<div className="flex py-2 px-1 justify-between">*/}
                                {/*    <Checkbox*/}
                                {/*        classNames={{*/}
                                {/*            label: "text-small",*/}
                                {/*        }}*/}
                                {/*    >*/}
                                {/*        Remember me*/}
                                {/*    </Checkbox>*/}
                                {/*    <Link color="primary" href="#" size="sm">*/}
                                {/*        Forgot password?*/}
                                {/*    </Link>*/}
                                {/*</div>*/}
                            </ModalBody>
                            <ModalFooter>
                                <Button color="danger" variant="flat" onPress={onClose}>
                                    Close
                                </Button>
                                <Button onPress={onClose}>
                                    Sign in
                                </Button>
                            </ModalFooter>
                        </>
                    )}
                </ModalContent>
            </Modal>
        </>
    );
}
