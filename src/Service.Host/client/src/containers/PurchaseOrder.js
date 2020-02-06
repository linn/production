import { connect } from 'react-redux';
import {
    getItemErrorDetailMessage,
    initialiseOnMount
} from '@linn-it/linn-form-components-library';
import PurchaseOrder from '../components/PurchaseOrder';
import issueSernosActions from '../actions/issueSernosActions';
import purchaseOrderActions from '../actions/purchaseOrderActions';
import purchaseOrderSelectors from '../selectors/purchaseOrderSelectors';
import issueSernosSelectors from '../selectors/issueSernosSelectors';
import * as itemTypes from '../itemTypes';

const mapStateToProps = (state, { match }) => ({
    item: purchaseOrderSelectors.getItem(state),
    itemId: match.params.id,
    itemLoading: purchaseOrderSelectors.getLoading(state),
    snackbarVisible: issueSernosSelectors.getMessageVisible(state),
    message: issueSernosSelectors.getMessageText(state),
    itemError: getItemErrorDetailMessage(state, itemTypes.purchaseOrder.item)
});

const initialise = ({ itemId }) => dispatch => {
    dispatch(purchaseOrderActions.fetch(itemId));
};

const mapDispatchToProps = {
    initialise,
    issueSernos: issueSernosActions.requestProcessStart
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(PurchaseOrder));
