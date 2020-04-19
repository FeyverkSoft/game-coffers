import React from 'react';
import { useState } from "react";
import { Select, Button, Tooltip } from "antd"
import { SaveFilled, CloseOutlined } from '@ant-design/icons';
import { IF } from '../../_helpers';
import { Lang } from '../../_services';

export interface IItem {
    value: string;
    description: string;
}

export interface EditableSelectProps {
    items: Array<IItem>;
    value: string;
    onSave(value: string): void;
}

export const EditableSelect = ({ ...props }: EditableSelectProps) => {
    const [edited, toggleEdited] = useState(false);
    const [selectedValue, changeValue] = useState(props.value);
    return <div>
        <IF value={edited}>
            <Select
                size='middle'
                defaultValue={props.value}
                onChange={(value) => changeValue(value)}
            >
                {
                    props.items.map(t => <Select.Option key={t.value} value={t.value}>{t.description}</Select.Option>)
                }
            </Select>
            <Tooltip title={Lang('SAVE')}>
                <Button
                    type='link'
                    onClick={() => {
                        toggleEdited(false);
                        props.onSave(selectedValue);
                    }}
                    icon={<SaveFilled />}
                />
            </Tooltip>
            <Tooltip title={Lang('CANCEL')}>
                <Button
                    type='link'
                    onClick={() => toggleEdited(false)}
                    icon={<CloseOutlined />}
                />
            </Tooltip>
        </IF>
        <IF value={!edited}>
            <Button
                type='link'
                size='small'
                onClick={() => toggleEdited(!edited)}
            >
                {props.items.filter(_ => _.value === props.value)[0].description}
            </Button>
        </IF>
    </div>
}