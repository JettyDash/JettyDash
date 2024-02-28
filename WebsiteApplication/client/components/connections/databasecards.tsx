import React from 'react';
import {RadioGroup} from "@nextui-org/react";
import {Customradio} from "@/components/connections/customradio";
import Image from "next/image";

interface DatabaseCardsProps {
    imageSizeClasses: string;
    baseCardClasses: string;
    onDatabaseSelect: (database: string) => void; // Add callback function

}
 // Assigning the handler function

export const DatabaseCards: React.FC<DatabaseCardsProps> = ({ imageSizeClasses, baseCardClasses   , onDatabaseSelect }) => {
    const handleSelectionChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        const selectedDatabase = event.target.value;
        onDatabaseSelect(selectedDatabase); // Pass selected database to parent
    };

    return (
        <RadioGroup onChange={handleSelectionChange} className={"gap-1 mt-10 mb-3"} isRequired={true} orientation="horizontal" description={<p>
            Please select a database type <span className="text-red-500">*</span></p>}>
            <Customradio className={"group"} classNames={{base:baseCardClasses,  labelWrapper: "m-0"}} value="MYSQL">
                <div className="flex flex-col items-center">
                    <Image className={`${imageSizeClasses} group-hover:animate-pulse dark:invert`} height={"40"} width={"40"}
                           src={"../assets/icons/mysql.svg"} alt={"mysql"}/>
                    <p className={"group-hover:animate-pulse"}>mysql</p>
                </div>
            </Customradio>
            <Customradio className={"group"} classNames={{base:baseCardClasses, labelWrapper: "m-0"}} value="POSTGRES">
                <div className="flex flex-col items-center">
                    <Image className={`${imageSizeClasses} group-hover:animate-pulse dark:invert`} height={"40"} width={"40"}
                           src={"../assets/icons/postgres.svg"} alt={"postgres"}/>
                    <p className={"group-hover:animate-pulse"}>postgres</p>
                </div>
            </Customradio>
            <Customradio className={"group"} classNames={{base:baseCardClasses, labelWrapper: "m-0"}} value="MSSQL">
                <div className="flex flex-col items-center">
                    <Image className={`${imageSizeClasses} group-hover:animate-pulse dark:invert`} height={"40"} width={"40"}
                           src={"../assets/icons/mssql.svg"} alt={"mssql"}/>
                    <p className={"group-hover:animate-pulse"}>mssql</p>
                </div>
            </Customradio>
            <Customradio className={"group"} classNames={{base:baseCardClasses, labelWrapper: "m-0"}} value="ORACLE">
                <div className="flex flex-col items-center">
                    <Image className={`${imageSizeClasses} group-hover:animate-pulse dark:invert`} height={"40"} width={"40"}
                           src={"../assets/icons/oracle.svg"} alt={"oracle"}/>
                    <p className={"group-hover:animate-pulse"}>oracle</p>
                </div>
            </Customradio>
        </RadioGroup>
    );
};
