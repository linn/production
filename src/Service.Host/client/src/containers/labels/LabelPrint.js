import { connect } from 'react-redux';
import { getItemErrorDetailMessage } from '@linn-it/linn-form-components-library';
import LabelPrint from '../../components/labels/LabelPrint';
import serialNumberActions from '../../actions/serialNumberActions';
import serialNumberSelectors from '../../selectors/serialNumberSelectors';
import printAllLabelsForProductActions from '../../actions/printAllLabelsForProductActions';
import printAllLabelsForProductSelectors from '../../selectors/printAllLabelsForProductSelectors';
import printMACLabelsActions from '../../actions/printMACLabelsActions';
import printMACLabelsSelectors from '../../selectors/printMACLabelsSelectors';
import * as processTypes from '../../processTypes';
import * as itemTypes from '../../itemTypes';

const mapStateToProps = state => ({
    printAllLabelsForProductErrorDetail: getItemErrorDetailMessage(
        state,
        processTypes.printAllLabelsForProduct.item
    ),
    printMACLabelsErrorDetail: getItemErrorDetailMessage(state, processTypes.printMACLabels.item),
    serialNumberErrorDetail: getItemErrorDetailMessage(state, itemTypes.serialNumbers.item),
    serialNumbers: serialNumberSelectors.getItems(state),
    serialNumbersLoading: serialNumberSelectors.getLoading(state),
    printAllLabelsForProductMessageVisible: printAllLabelsForProductSelectors.getMessageVisible(
        state
    ),
    printAllLabelsForProductMessageText: printAllLabelsForProductSelectors.getMessageText(state),
    printMACLabelsMessageVisible: printMACLabelsSelectors.getMessageVisible(state),
    printMACLabelsMessageText: printMACLabelsSelectors.getMessageText(state)
});

const mapDispatchToProps = {
    fetchSerialNumbers: serialNumberActions.fetchByQueryString,
    setPrintAllLabelsForProductActionsMessageVisible:
        printAllLabelsForProductActions.setMessageVisible,
    printAllLabelsForProduct: printAllLabelsForProductActions.requestProcessStart,
    setPrintMACLabelsActionsMessageVisible: printMACLabelsActions.setMessageVisible,
    printMACLabels: printMACLabelsActions.requestProcessStart,
    clearMacLabelErrors: printMACLabelsActions.clearErrorsForItem,
    clearAllLabelErrors: printAllLabelsForProductActions.clearErrorsForItem
};

export default connect(mapStateToProps, mapDispatchToProps)(LabelPrint);
