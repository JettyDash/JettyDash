import React from 'react';

const Createdatabase = () => {
    return (
        <div className="flex flex-initial flex-col items-center justify-start p-0 gap-3 mt-2">
            <div className="flex items-center justify-center border-2 border-solid border-zinc-700 bg-black size-16 rounded-xl">
                <svg className="size-10" fill="none" xmlns="http://www.w3.org/2000/svg">
                    <defs>
                        <linearGradient gradientUnits="userSpaceOnUse" id="a" x1="4.50111"
                                        x2="31.3716" y1="34.551" y2="6.09993">
                            <stop stopColor="#999"></stop>
                            <stop offset="1" stopColor="#444"></stop>
                        </linearGradient>
                    </defs>
                    <path
                        d="M33 7.65C33 10.2181 26.732 12.3 19 12.3C11.268 12.3 5 10.2181 5 7.65M33 7.65C33 5.08188 26.732 3 19 3C11.268 3 5 5.08188 5 7.65M33 7.65C33 11.8872 33 14.2628 33 18.5M5 7.65C5 11.8872 5 14.2628 5 18.5M33 18.5C33 21.073 26.7778 23.15 19 23.15C11.2222 23.15 5 21.073 5 18.5M33 18.5C33 22.7372 33 29.35 33 29.35C33 31.923 26.7778 34 19 34C11.2222 34 5 31.923 5 29.35C5 29.35 5 22.7372 5 18.5"
                        stroke="url(#a" strokeWidth="2">
                    </path>
                </svg>
            </div>
            <div className="flex flex-initial flex-col items-center justify-start p-0 gap-4">
                <p className="text-gray-100 text-base font-medium">Create a database</p>
                <p className="text-dark-400 text-base font-normal text-center max-w-[282px]">Create databases and stores
                    that you can connect to your team's projects.</p>
            </div>
        </div>


    );
};

export default Createdatabase;