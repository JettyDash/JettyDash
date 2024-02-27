import React from 'react';
import {RadioGroup} from "@nextui-org/react";
import {CustomRadio} from "@/app/(root)/datamodels/customRadio";
import Image from "next/image";

interface DatabaseCardsProps {
    imageSizeClasses: string;
    baseCardClasses: string;
}

export const DatabaseCards: React.FC<DatabaseCardsProps> = ({ imageSizeClasses, baseCardClasses }) => {
    return (
        <RadioGroup className={"gap-1"} isRequired={true} orientation="horizontal" description={"Please select a database type"}>
            <CustomRadio className={"group"} classNames={{base:baseCardClasses,  labelWrapper: "m-0"}} value="MYSQL">
                <div className="flex flex-col items-center">
                    <Image className={`${imageSizeClasses} group-hover:animate-pulse dark:invert`} height={"40"} width={"40"}
                           src={"../assets/icons/mysql.svg"} alt={"mysql"}/>
                    <p className={"group-hover:animate-pulse"}>mysql</p>
                </div>
            </CustomRadio>
            <CustomRadio className={"group"} classNames={{base:baseCardClasses, labelWrapper: "m-0"}} value="POSTGRES">
                <div className="flex flex-col items-center">
                    <Image className={`${imageSizeClasses} group-hover:animate-pulse dark:invert`} height={"40"} width={"40"}
                           src={"../assets/icons/postgres.svg"} alt={"postgres"}/>
                    <p className={"group-hover:animate-pulse"}>postgres</p>
                </div>
            </CustomRadio>
            <CustomRadio className={"group"} classNames={{base:baseCardClasses, labelWrapper: "m-0"}} value="MSSQL">
                <div className="flex flex-col items-center">
                    <Image className={`${imageSizeClasses} group-hover:animate-pulse dark:invert`} height={"40"} width={"40"}
                           src={"../assets/icons/mssql.svg"} alt={"mssql"}/>
                    <p className={"group-hover:animate-pulse"}>mssql</p>
                </div>
            </CustomRadio>
            <CustomRadio className={"group"} classNames={{base:baseCardClasses, labelWrapper: "m-0"}} value="ORACLE">
                <div className="flex flex-col items-center">
                    <Image className={`${imageSizeClasses} group-hover:animate-pulse dark:invert`} height={"40"} width={"40"}
                           src={"../assets/icons/oracle.svg"} alt={"oracle"}/>
                    <p className={"group-hover:animate-pulse"}>oracle</p>
                </div>
            </CustomRadio>
        </RadioGroup>
    );
};
