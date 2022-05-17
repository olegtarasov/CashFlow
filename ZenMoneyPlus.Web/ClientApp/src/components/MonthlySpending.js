import DropdownTreeSelect from 'react-dropdown-tree-select'
import 'react-dropdown-tree-select/dist/styles.css'
import React from 'react';

const data = {
    label: 'search me',
    value: 'searchme',
    children: [
        {
            label: 'search me too',
            value: 'searchmetoo',
            children: [
                {
                    label: 'No one can get me',
                    value: 'anonymous',
                },
            ],
        },
    ],
}

export function MontlySpending() {
    return (
        <>
            <DropdownTreeSelect data={data}/>
        </>
    );
}
