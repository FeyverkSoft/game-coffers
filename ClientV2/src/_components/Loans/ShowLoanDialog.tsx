import React from 'react';
import { Modal, Descriptions, Button } from 'antd';
import { Lang, ILoanView, DLang } from '../../_services';
import { formatDateTime } from '../../_helpers/formatDateTime';
import { connect } from 'react-redux';
import { IStore } from '../../_helpers/store';
import { Private } from '../Private';
import { IF } from '../../_helpers';
import { gamerInstance } from '../../_actions/gamer/gamer.actions';
import { IHolded } from '../../core';

interface ILoanDialogProps {
    loan: ILoanView & IHolded;
    visible?: boolean;
    onClose(): void;
    onCancel(loanId: string): void;
    onProlong(loanId: string): void;
    onReverse(loanId: string): void;
}

const showLoanDialog = ({ ...props }: ILoanDialogProps) => {
    const { loan } = props;
    return (
        <Modal
            title={Lang('SHOW_LOAN_MODAL')}
            visible={props.visible}
            confirmLoading={loan.holding}
            footer={
                <Private roles={['admin', 'leader', 'officer']}>
                    <IF value={loan.loanStatus === 'Active'}>
                        <Button
                            danger={true}
                            onClick={() => props.onCancel(loan.id)}
                            loading={loan.holding}
                        >
                            {Lang('CANCEL')}
                        </Button>
                    </IF>
                    <IF value={loan.loanStatus === 'Active' || loan.loanStatus === 'Expired'}>
                        <Button
                            danger={true}
                            onClick={() => props.onProlong(loan.id)}
                            loading={loan.holding}
                        >
                            {Lang('PROLONG')}
                        </Button>
                    </IF>
                </Private>
            }
            onCancel={() => props.onClose()}
        >
            <Descriptions colon={true} column={1}>
                <Descriptions.Item label={Lang("MODAL_LOAN_AMOUNT")} span={1}>
                    {String(loan.amount)}
                </Descriptions.Item>
                <Descriptions.Item label={Lang("MODAL_LOAN_BALANCE")} span={1}>
                    {String(loan.balance)}
                </Descriptions.Item>
                <Descriptions.Item label={Lang("MODAL_LOAN_DATE")} span={1}>
                    {formatDateTime(loan.createDate, 'd')}
                </Descriptions.Item>
                <Descriptions.Item label={Lang("MODAL_LOAN_EXPIREDDATE")} span={1}>
                    {formatDateTime(loan.expiredDate, 'd')}
                </Descriptions.Item>
                <Descriptions.Item label={Lang("MODAL_LOAN_DESCRIPTION")} span={1}>
                    {loan.description}
                </Descriptions.Item>
                <Descriptions.Item label={Lang("MODAL_LOAN_STATUS")} span={1}>
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
            onProlong: (loanId: string) => dispatch(gamerInstance.prolongLoan({ loanId: loanId })),
        }
    }
)(showLoanDialog);

export { connectedShowLoanDialog as ShowLoanDialog };