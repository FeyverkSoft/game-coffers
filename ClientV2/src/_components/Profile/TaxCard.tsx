import * as React from 'react';
import style from './profile.module.scss';
import { Row, Col, Statistic, PageHeader, Steps } from 'antd';
import { Lang, ITax } from '../../_services';
import { Card } from '../Base/Card';

export interface ITaxCard {
    tax: ITax;
    charCount: number;
    loading?: boolean;
}

export const TaxCard = ({ ...props }: ITaxCard) => <Card
    loading={props.loading}
>
    <Row className={style['taxcard']}>
        <Col xs={7} sm={7} md={5} lg={3} xl={4}>
            <Statistic
                title={Lang('USER_TAX_AMOUNT')}
                value={props.tax.taxAmount}
                precision={2}
                suffix="G"
            />
        </Col>
        <Col xs={17} sm={17} md={19} lg={21} xl={20}>
            <PageHeader
                subTitle={Lang('USER_SALE')}
                title=''
                style={{ padding: '0px 12px', margin: '0px' }}
            >
                <Steps
                    direction='horizontal'
                    progressDot
                    size='small'
                    current={props.charCount - 1}>
                    {props.tax.taxTariff.map((_, i) => <Steps.Step
                        key={i}
                        title={_}
                        className={style['step']}
                    />)}
                </Steps>
            </PageHeader>
        </Col>
    </Row>
</Card>;