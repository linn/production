import { connect } from 'react-redux';
import { getItemErrors } from '@linn-it/linn-form-components-library';
import queryString from 'query-string';
import salesArticleActions from '../../actions/salesArticleActions';
import salesArticleSelectors from '../../selectors/salesArticleSelectors';
import SerialNumberReissue from '../../components/serialNumberReissue/SerialNumberReissue';
import serialNumberReissueActions from '../../actions/serialNumberReissueActions';
import serialNumberReissueSelectors from '../../selectors/serialNumberReissueSelectors';
import salesArticlesActions from '../../actions/salesArticlesActions';
import salesArticlesSelectors from '../../selectors/salesArticlesSelectors';
import serialNumberActions from '../../actions/serialNumberActions';
import serialNumberSelectors from '../../selectors/serialNumberSelectors';

const getOptions = ownProps => {
    const options = queryString.parse(ownProps.location.search);
    return options;
};

const mapStateToProps = (state, ownProps) => ({
    editStatus: serialNumberReissueSelectors.getEditStatus(state),
    itemErrors: getItemErrors(state),
    item: serialNumberReissueSelectors.getItem(state),
    loading: serialNumberReissueSelectors.getLoading(state),
    salesArticle: salesArticleSelectors.getItem(state),
    salesArticleSearchResults: salesArticlesSelectors
        .getSearchItems(state)
        .map(s => ({ ...s, id: s.articleNumber, name: s.articleNumber })),
    salesArticlesSearchLoading: salesArticlesSelectors.getSearchLoading(state),
    serialNumbers: serialNumberSelectors.getItems(state),
    serialNumbersLoading: serialNumberSelectors.getLoading(state),
    snackbarVisible: serialNumberReissueSelectors.getSnackbarVisible(state),
    reissuedSerialNumber: serialNumberReissueSelectors.getItem(state),
    options: getOptions(ownProps)
});

const mapDispatchToProps = {
    addItem: serialNumberReissueActions.add,
    fetchSerialNumbers: serialNumberActions.fetchByQueryString,
    fetchSalesArticle: salesArticleActions.fetch,
    searchSalesArticles: salesArticlesActions.search,
    clearSalesArticlesSearch: salesArticlesActions.clearSearch,
    setEditStatus: serialNumberReissueActions.setEditStatus,
    setSnackbarVisible: serialNumberReissueActions.setSnackbarVisible
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(SerialNumberReissue);
