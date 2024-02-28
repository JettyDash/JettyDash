'use client';
import React from "react";
import {
    Modal,
    ModalContent,
    ModalHeader,
    ModalBody,
    ModalFooter,
    Button,
    Checkbox,
    Input,
    Link,
    Card, CardBody, Tabs, Tab, RadioGroup, Radio, useDisclosure
} from "@nextui-org/react";
import {PlusIcon} from "@/components/icons/PlusIcon";
import {Key} from "@react-types/shared";



export default function DataModels() {
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
                placement="bottom-center" // center on desktop, bottom on mobile
            >
                <ModalContent className="flex items-center w-[800px] h-[600px]">
                    {(onClose) => (
                        <>
                            <ModalHeader className="flex flex-col gap-1">New Connection</ModalHeader>
                            <ModalBody className="w-full h-full">

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
