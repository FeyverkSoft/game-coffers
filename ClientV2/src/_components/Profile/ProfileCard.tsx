import * as React from 'react';
import { Avatar, Row } from "antd";
import { IProfile, DLang, Lang } from "../../_services";
import { IF } from '../../_helpers'
import { Card } from '../Base/Card';

const colorArray = ['#FF6633', '#FFB399', '#FF33FF', '#FFFF99', '#00B3E6',
    '#E6B333', '#3366E6', '#999966', '#99FF99', '#B34D4D',
    '#80B300', '#809900', '#E6B3B3', '#6680B3', '#66991A',
    '#FF99E6', '#CCFF1A', '#FF1A66', '#E6331A', '#33FFCC',
    '#66994D', '#B366CC', '#4D8000', '#B33300', '#CC80CC',
    '#66664D', '#991AFF', '#E666FF', '#4DB3FF', '#1AB399',
    '#E666B3', '#33991A', '#CC9999', '#B3B31A', '#00E680',
    '#4D8066', '#809980', '#E6FF80', '#1AFF33', '#999933',
    '#FF3380', '#CCCC00', '#66E64D', '#4D80CC', '#9900B3',
    '#E64D66', '#4DB380', '#FF4D4D', '#99E6E6', '#6666FF'];

export interface IProfileCardProps extends React.Props<any> {
    isLoading: boolean,
    profile: IProfile;
}

export const ProfileCard = ({ ...props }: IProfileCardProps) => {
    let { profile } = props;
    let { month, day, year } = {
        month: profile.dateOfBirth.getMonth() + 1,
        day: profile.dateOfBirth.getDay(),
        year: profile.dateOfBirth.getFullYear()
    }
    let color = colorArray[profile.name.charCodeAt(0) % colorArray.length];
    return (<Card
        loading={props.isLoading}
        style={{
            background: 'linear-gradient(29deg, rgba(255,242,220,1) 0%, rgba(233,233,255,1) 40%, rgba(239,223,255,1) 100%)',
        }}
    >
        <Row gutter={[16, 16]} justify='center' align={'middle'}>
            <Avatar
                size='large'
                style={{
                    backgroundColor: color
                }}
            >
                {profile.name[0]}
            </Avatar>
        </Row>
        <Row gutter={[16, 16]} justify='center' align='middle'>
            <span style={{
                fontWeight: 500
            }}>{profile.name}&nbsp;
                <IF value={profile.characterName}>
                    -&nbsp;{profile.characterName}
                </IF>
            </span>
        </Row>
        <Row gutter={[16, 16]} justify='center' align='middle'>
            {DLang('USER_ROLE', profile.rank)},&nbsp;
            {Lang('DATEOFBIRTH')}:&nbsp;
            {`${year}-${month + 1 > 9 ? month : '0' + month}-${day > 9 ? day : '0' + day}`}
        </Row>
    </Card>)
}
