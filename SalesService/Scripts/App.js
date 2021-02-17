var ViewModel = function () {
    var self = this;
    self.produtoes = ko.observableArray();
    self.error = ko.observable();
    self.detail = ko.observable();
    self.revendedors = ko.observableArray();
    self.newProduto = {
        Revendedor: ko.observable(),
        Name: ko.observable(),
        Price: ko.observable(),
        Category: ko.observable(),
        Description: ko.observable()
    }

    var revendedorsUri = '/api/revendodors/';

    function getRevendedors() {
        ajaxHelper(revendedorsUri, 'GET').done(function (data) {
            self.revendedors(data);
        });
    }

    self.addProduto = function (formElement) {
        var produto = {
            RevendedorId: self.newBook.Revendedor().Id,
            Name: self.newProduto.Name(),
            Price: self.newBook.Price(),
            Category: self.newProduto.Category(),
            Description: self.newProduto.Category()
        };

        ajaxHelper(produtoesUri, 'POST', produto).done(function (item) {
            self.produtoes.push(item);
        });
    }

    getRevendedors();

    var produtoesUri = '/api/produtoes/';

    function ajaxHelper(uri, method, data) {
        self.error(''); // Clear error message
        return $.ajax({
            type: method,
            url: uri,
            dataType: 'json',
            contentType: 'application/json',
            data: data ? JSON.stringify(data) : null
        }).fail(function (jqXHR, textStatus, errorThrown) {
            self.error(errorThrown);
        });
    }

    function getAllProdutoes() {
        ajaxHelper(produtoesUri, 'GET').done(function (data) {
            self.produtoes(data);
        });
    }

    self.getProdutoDetail = function (item) {
        ajaxHelper(produtoesUri + item.Id, 'GET').done(function (data) {
            self.detail(data);
        });
    }

    // Fetch the initial data.
    getAllProdutoes();
};

ko.applyBindings(new ViewModel());

