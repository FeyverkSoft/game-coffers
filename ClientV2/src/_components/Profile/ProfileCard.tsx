import * as React from 'react';
import { Button, Icon, Card } from "antd";
import { IProfile, Lang } from "../../_services";

export interface IProfileCardProps extends React.Props<any> {
    isLoading: boolean,
    profile: IProfile;
}

export const ProfileCard = ({ ...props }: IProfileCardProps) => {
    return (<Card
        loading={props.isLoading}
        title={Lang("PROFILE_PAGE")}
    >
    </Card>)
}
