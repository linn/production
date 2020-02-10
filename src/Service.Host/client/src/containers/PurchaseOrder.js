import { connect } from 'react-redux';
import {
    getItemErrorDetailMessage,
    getItemError,
    initialiseOnMount
} from '@linn-it/linn-form-components-library';
import PurchaseOrder from '../components/PurchaseOrder';
import issueSernosActions from '../actions/issueSernosActions';
import buildSernosActions from '../actions/buildSernosActions';
import purchaseOrderActions from '../actions/purchaseOrderActions';
import purchaseOrderSelectors from '../selectors/purchaseOrderSelectors';
import issueSernosSelectors from '../selectors/issueSernosSelectors';
import buildSernosSelectors from '../selectors/buildSernosSelectors';
import * as itemTypes from '../itemTypes';
import * as processTypes from '../processTypes';

const mapStateToProps = (state, { match }) => ({
    item: purchaseOrderSelectors.getItem(state),
    itemId: match.params.id,
    editStatus: purchaseOrderSelectors.getEditStatus(state),
    itemLoading: purchaseOrderSelectors.getLoading(state),
    snackbarVisible: purchaseOrderSelectors.getSnackbarVisible(state),
    issueMessage: issueSernosSelectors.getMessageText(state),
    issueSernosSnackbarVisible: issueSernosSelectors.getMessageVisible(state),
    buildSernosSnackbarVisible: buildSernosSelectors.getMessageVisible(state),
    buildMessage: buildSernosSelectors.getMessageText(state),
    itemError: getItemErrorDetailMessage(state, itemTypes.purchaseOrder.item),
    buildError: getItemError(state, processTypes.buildSernos),
    issueError: getItemError(state, processTypes.issueSernos),
    issueRequested: issueSernosSelectors.getWorking(state),
    buildRequested: buildSernosSelectors.getWorking(state)
});

const initialise = ({ itemId }) => dispatch => {
    dispatch(purchaseOrderActions.fetch(itemId));
};

const mapDispatchToProps = {
    initialise,
    issueSernos: issueSernosActions.requestProcessStart,
    buildSernos: buildSernosActions.requestProcessStart,
    setEditStatus: purchaseOrderActions.setEditStatus,
    setSnackbarVisible: purchaseOrderActions.setSnackbarVisible,
    updatePurchaseOrder: purchaseOrderActions.update,
    setIssueMessageVisible: issueSernosActions.setMessageVisible,
    setBuildMessageVisible: buildSernosActions.setMessageVisible
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(PurchaseOrder));
