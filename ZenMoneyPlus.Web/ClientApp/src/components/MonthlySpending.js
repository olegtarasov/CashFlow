import DropdownTreeSelect from 'react-dropdown-tree-select'
import 'react-dropdown-tree-select/dist/styles.css'
import React, {useEffect, useState} from 'react';
import axios from "axios";
import {useErrorContext} from "../context/errorContext";

function mapTag(tag) {
    return {label: tag.title, value: tag.id, children: tag.childrenTags.map(mapTag), checked: true};
}

export function MontlySpending() {
    const errorContext = useErrorContext();
    const [tags, setTags] = useState({});

    useEffect(() => {
        axios.get("api/Tags?mode=outcome")
            .then(response => {
                setTags(response.data.map(mapTag));
            })
            .catch(error => {
                errorContext.processError(error);
            });
    }, [errorContext]);

    return (
        <>
            <DropdownTreeSelect data={tags}/>
        </>
    );
}
