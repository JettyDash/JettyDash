
import React from "react";
import {Modal, ModalContent, ModalHeader, ModalBody, ModalFooter, Button, useDisclosure} from "@nextui-org/react";
import {PlusIcon} from "@/components/icons/PlusIcon";
import {ConnectionForm} from "@/components/connections/connectionform";
import {Divider} from "@nextui-org/divider";

const CreateNewDatabase = () => {

    const {isOpen, onOpen, onOpenChange} = useDisclosure();


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
            <Modal isOpen={isOpen} onOpenChange={onOpenChange} isDismissable={false} isKeyboardDismissDisabled={true}>
                <ModalContent>
                    {(onClose) => (
                        <>
                            <ModalHeader className="flex flex-col gap-1 font-ubuntu items-center ">Create a New Connection</ModalHeader>
                            <Divider/>
                            <ModalBody>
                                <ConnectionForm/>
                            </ModalBody>
                            <ModalFooter>
                                <Button color="danger" variant="light" onPress={onClose}>
                                    Close
                                </Button>
                                <Button color="primary" onPress={onClose}>
                                    Action
                                </Button>
                            </ModalFooter>
                        </>
                    )}
                </ModalContent>
            </Modal>
        </>
    );
}

export default CreateNewDatabase;
