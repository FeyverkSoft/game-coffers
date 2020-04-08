import React from 'react';
import { Modal, Descriptions, Button } from 'antd';
import { Lang, ILoanView, DLang } from '../../_services';
import { formatDateTime } from '../../_helpers/formatDateTime';
import { connect } from 'react-redux';
import { IStore } from '../../_helpers/store';
import { Private } from '../Private';
import { IF } from '../../_helpers';
import { gamerInstance } from '../../_actions';
import { IHolded } from '../../core';

interface ILoanDialogProps {
    loan: ILoanView & IHolded;
    visible?: boolean;
    onClose(): void;
    onCancel(loanId: string): void;
    onReverse(loanId: string): void;
}

const showLoanDialog = ({ ...props }: ILoanDialogProps) => {
    const { loan } = props;
    return (
        <Modal
            title={Lang('SHOW_LOAN_MODAL')}
            visible={props.visible}
            footer={
                <Private roles={['admin', 'leader', 'officer']}>
                    <IF value={loan.loanStatus === 'Active'}>
                        <Button
                            type='danger'
                            onClick={() => props.onCancel(loan.id)}
                            loading={loan.holding}
                        >
                            {Lang('CANCEL')}
                        </Button>
                    </IF>
                </Private>
            }
            onCancel={() => props.onClose()}
        >
            <Descriptions colon={true}>
                <Descriptions.Item label={Lang("MODAL_LOAN_AMOUNT")} span={12}>
                    {String(loan.amount)}
                </Descriptions.Item>
                <Descriptions.Item label={Lang("MODAL_LOAN_BALANCE")} span={12}>
                    {String(loan.balance)}
                </Descriptions.Item>
                <Descriptions.Item label={Lang("MODAL_LOAN_DATE")} span={12}>
                    {formatDateTime(loan.createDate, 'd')}
                </Descriptions.Item>
                <Descriptions.Item label={Lang("MODAL_LOAN_EXPIREDDATE")} span={12}>
                    {formatDateTime(loan.expiredDate, 'd')}
                </Descriptions.Item>
                <Descriptions.Item label={Lang("MODAL_LOAN_DESCRIPTION")} span={12}>
                    {loan.description}
                </Descriptions.Item>
                <Descriptions.Item label={Lang("MODAL_LOAN_STATUS")} span={12}>
                    {DLang('LOAN_STATUS', loan.loanStatus)}
                </Descriptions.Item>
            </Descriptions>

            {/* <Col className={style['operation-list']}>
                    <NamedValue name={Lang("MODAL__OPERATIONS")}>
                        {operations.items.map(_ => (
                            <div
                                key={_.id}
                                title={_.description}
                                className={style['operations']}
                            >
                                {_.amount}
                            </div>
                        ))}
                    </NamedValue>
                        </Col>*/}
        </Modal>
    );
}

interface ShowLoanDialogProps {
    loanId?: string;
}
const connectedShowLoanDialog = connect<{}, {}, any, IStore>(
    (state: IStore, props: ShowLoanDialogProps) => {
        const loans = state.gamers.loans;
        let loan = {};
        if (props.loanId && loans)
            loan = loans[props.loanId];
        return {
            loan: loan
        };
    },
    (dispatch: any) => {
        return {
            onCancel: (loanId: string) => dispatch(gamerInstance.cancelLoan({ loanId: loanId })),
        }
    }
)(showLoanDialog);

export { connectedShowLoanDialog as ShowLoanDialog };