import { connect } from './node_modules/react-redux';
import { getItemError, initialiseOnMount } from './node_modules/@linn-it/linn-form-components-library';
import ViewLabelTypes from '../../components/labelTypes/LabelTypes';
import labelTypesActions from '../../actions/labelTypesActions';
import labelTypesSelectors from '../../selectors/labelTypesSelectors';
import * as itemTypes from '../../itemTypes';

const mapStateToProps = state => ({
    items: labelTypesSelectors.getItems(state),
    loading: labelTypesSelectors.getLoading(state),
    itemError: getItemError(state, itemTypes.labelTypes.item)
});

const initialise = () => dispatch => {
    dispatch(labelTypesActions.fetch());
};

const mapDispatchToProps = {
    initialise
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(initialiseOnMount(ViewLabelTypes));
